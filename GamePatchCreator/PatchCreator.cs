using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VCDiff.Encoders;
using VCDiff.Includes;

namespace GamePatchCreator
{
    public class PatchCreator
    {

        private void DoEncode(string outPatchFile, string oldFile, string newFile)
        {
            using (FileStream output = new FileStream(outPatchFile, FileMode.Create, FileAccess.Write))
            using (FileStream dict = new FileStream(oldFile, FileMode.Open, FileAccess.Read))
            using (FileStream target = new FileStream(newFile, FileMode.Open,FileAccess.Read))
            {
                VCCoder coder = new VCCoder(dict, target, output);
                VCDiffResult result = coder.Encode(); //encodes with no checksum and not interleaved
                if (result != VCDiffResult.SUCCESS)
                {
                    throw new Exception("DoEncode was not able to encode properly file: " + Path.GetFileName(oldFile));
                }
            }
        }

        private void GetAllFilesInDirectoryRec(string dir,List<String> files)
        {
            files.AddRange(Directory.GetFiles(dir));
            foreach(string subDir in Directory.GetDirectories(dir))
            {
                GetAllFilesInDirectoryRec(subDir, files);
            }
        }

        private string[] GetAllFilesInDirectory(string dir)
        {
            List<String> files=new List<string>();
            GetAllFilesInDirectoryRec(dir, files);
            return files.ToArray();
        }

        private bool isPathInsidePath(string dir1,string dir2)
        {
            return StringExtensions.IsSubPathOf(dir1, dir2) || StringExtensions.IsSubPathOf(dir2,dir1);
        }

        private string GetSha1FromFile(string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open))
            using (BufferedStream bs = new BufferedStream(fs))
            {
                using (SHA1Managed sha1 = new SHA1Managed())
                {
                    byte[] hash = sha1.ComputeHash(bs);
                    StringBuilder formatted = new StringBuilder(2 * hash.Length);
                    foreach (byte b in hash)
                    {
                        formatted.AppendFormat("{0:X2}", b);
                        
                    }
                    return formatted.ToString();
                }
            }
        }

        private bool FilesAreEqual(string file1,string file2)
        {
            FileInfo fi1 = new FileInfo(file1);
            FileInfo fi2 = new FileInfo(file2);
            return FilesAreEqual(fi1, fi2);
            
        }


        private bool FilesAreEqual(FileInfo first, FileInfo second)
        {
            const int BYTES_TO_READ = sizeof(Int64);
            if (first.Length != second.Length)
                return false;

            if (string.Equals(first.FullName, second.FullName, StringComparison.OrdinalIgnoreCase))
                return true;

            int iterations = (int)Math.Ceiling((double)first.Length / BYTES_TO_READ);

            using (FileStream fs1 = first.OpenRead())
            using (FileStream fs2 = second.OpenRead())
            {
                byte[] one = new byte[BYTES_TO_READ];
                byte[] two = new byte[BYTES_TO_READ];

                for (int i = 0; i < iterations; i++)
                {
                    fs1.Read(one, 0, BYTES_TO_READ);
                    fs2.Read(two, 0, BYTES_TO_READ);

                    if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
                        return false;
                }
            }

            return true;
        }

        public string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        /// <summary>
        /// Returns a relative path string from a full path based on a base path
        /// provided.
        /// </summary>
        /// <param name="fullPath">The path to convert. Can be either a file or a directory</param>
        /// <param name="basePath">The base path on which relative processing is based. Should be a directory.</param>
        /// <returns>
        /// String of the relative path.
        /// 
        /// Examples of returned values:
        ///  test.txt, ..\test.txt, ..\..\..\test.txt, ., .., subdir\test.txt
        /// </returns>
        public static string GetRelativePath(string fullPath, string basePath)
        {
            // Require trailing backslash for path
            if (!basePath.EndsWith("\\"))
                basePath += "\\";

            Uri baseUri = new Uri(basePath);
            Uri fullUri = new Uri(fullPath);

            Uri relativeUri = baseUri.MakeRelativeUri(fullUri);

            // Uri's use forward slashes so convert back to backward slashes
            //return relativeUri.ToString().Replace("/", "\\");
            return Uri.UnescapeDataString( relativeUri.ToString()).Replace("/", "\\");

        }

        public void CreatePatch(string oldVersionPath, string newVersionPath, string outputFile)
        {
            if(isPathInsidePath(oldVersionPath,newVersionPath))
            {
                throw new ArgumentException("Paths overlapping");
            }

            List<String> oldFiles = new List<string>();
            foreach(string file in GetAllFilesInDirectory(oldVersionPath))
            {
                oldFiles.Add(GetRelativePath(file,oldVersionPath));
            }

            List<string> newFiles = new List<string>();
            foreach (string file in GetAllFilesInDirectory(newVersionPath))
            {
                newFiles.Add(GetRelativePath(file,newVersionPath));
            }

            string patchFolder = GetTemporaryDirectory();

            foreach(string newFileSubPath in newFiles)
            {
                string newFile = Path.Combine(newVersionPath, newFileSubPath);
                string oldFile = Path.Combine(oldVersionPath, newFileSubPath);

                if(!File.Exists(oldFile))
                {
                    string fileTocreate = Path.Combine(patchFolder, newFileSubPath + ".new");
                    Directory.CreateDirectory(Path.GetDirectoryName(fileTocreate));
                    File.Copy(newFile, fileTocreate);
                }else
                {
                    if(!FilesAreEqual(oldFile,newFile))
                    {
                        string fileTocreate = Path.Combine(patchFolder, newFileSubPath + ".upd");
                        Directory.CreateDirectory(Path.GetDirectoryName(fileTocreate));
                        DoEncode(fileTocreate, oldFile, newFile);
                    }
                }
            }

            //Get deleted files
            foreach(string oldFileSubPath in oldFiles)
            {
                string newFile = Path.Combine(newVersionPath, oldFileSubPath);
                if(!File.Exists(newFile))
                {
                    //Just create empty file as marker to remove the file while executing the patch.
                    string fileTocreate = Path.Combine(patchFolder, oldFileSubPath + ".del");
                    Directory.CreateDirectory(Path.GetDirectoryName(fileTocreate));
                    using (StreamWriter sw = new StreamWriter(fileTocreate)) ;
                }
            }

            ICSharpCode.SharpZipLib.Zip.FastZip zip = new ICSharpCode.SharpZipLib.Zip.FastZip();
            zip.CreateZip(outputFile, patchFolder, true, "");
            Directory.Delete(patchFolder,true);

        }
    }
}
