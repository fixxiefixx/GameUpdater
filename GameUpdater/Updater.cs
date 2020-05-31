﻿using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using Lib.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCDiff.Decoders;
using VCDiff.Includes;

namespace GameUpdater
{
    public class Updater
    {

        public class StatusUpdateArgs
        {
            public string statusText { get; set; }
            public int progressPercent { get; set; }
        }

        public event EventHandler<StatusUpdateArgs> OnStatusUpdate;

        private void StatusUpdate(string text,int percent)
        {
            OnStatusUpdate?.Invoke(this, new StatusUpdateArgs() { statusText = text, progressPercent = percent });
        }

        public string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        private string[] ReadVersionsFile(string file)
        {
            List<string> versions = new List<string>();
            using(StreamReader sr=new StreamReader(file))
            {
                string line;
                while((line=sr.ReadLine())!=null)
                {
                    versions.Add(line);
                }
            }
            return versions.ToArray();
        }

        private string ReadCurrentVersion()
        {
            
            string versionFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "version.txt");
            if(!File.Exists(versionFile))
            {
                return "";
            }
            using(StreamReader sr=new StreamReader(versionFile))
            {
                return sr.ReadLine();
            }
        }

        private void WriteCurrentVersion(string version)
        {
            string versionFile = Path.Combine(Path.GetDirectoryName( Application.ExecutablePath), "version.txt");
            using(StreamWriter sw=new StreamWriter(versionFile,false))
            {
                sw.WriteLine(version);
            }
        }

        void DoDecode(string outputFile, string oldFile, string patchFile)
        {
            using (FileStream output = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            using (FileStream dict = new FileStream(oldFile, FileMode.Open, FileAccess.Read))
            using (FileStream target = new FileStream(patchFile, FileMode.Open, FileAccess.Read))
            {
                VCDecoder decoder = new VCDecoder(dict, target, output);

                //You must call decoder.Start() first. The header of the delta file must be available before calling decoder.Start()

                VCDiffResult result = decoder.Start();

                if (result != VCDiffResult.SUCCESS)
                {
                    //error abort
                    throw new Exception("abort while decoding");
                }

                long bytesWritten = 0;
                result = decoder.Decode(out bytesWritten);

                if (result != VCDiffResult.SUCCESS)
                {
                    //error decoding
                    throw new Exception("Error decoding");
                }

                //if success bytesWritten will contain the number of bytes that were decoded
            }
        }

        private void InstallUpdateFromZip(string zipFile,string outFolder)
        {


            using(FileStream fs=new FileStream(zipFile,FileMode.Open))
            {
                using (var zipInputStream = new ZipInputStream(fs))
                {
                    while (zipInputStream.GetNextEntry() is ZipEntry zipEntry)
                    {
                        var entryFileName = zipEntry.Name;
                     


                        

                        // Manipulate the output filename here as desired.
                        var fullZipToPath = Path.Combine(outFolder, entryFileName);
                        var directoryName = Path.GetDirectoryName(fullZipToPath);
                        if (directoryName.Length > 0)
                            Directory.CreateDirectory(directoryName);

                        // Skip directory entry
                        if (Path.GetFileName(fullZipToPath).Length == 0)
                        {
                            continue;
                        }

                        if(entryFileName.EndsWith(".del"))
                        {
                            string fileToDelete = fullZipToPath.Substring(0, fullZipToPath.Length - 4);
                            File.Delete(fileToDelete);
                            continue;
                        }

                        if(entryFileName.EndsWith(".new"))
                        {
                            fullZipToPath = fullZipToPath.Substring(0, fullZipToPath.Length - 4);
                        }

                        // Unzip file in buffered chunks. This is just as fast as unpacking
                        // to a buffer the full size of the file, but does not waste memory.
                        // The "using" will close the stream even if an exception occurs.
                        using (FileStream streamWriter = File.Create(fullZipToPath))
                        {
                            // 4K is optimum
                            var buffer = new byte[4096];
                            StreamUtils.Copy(zipInputStream, streamWriter, buffer);
                        }

                        if(entryFileName.EndsWith(".upd"))
                        {
                            string oldFile= fullZipToPath.Substring(0, fullZipToPath.Length - 4);
                            string newFile = oldFile + ".new";
                            DoDecode(newFile, oldFile, fullZipToPath);
                            File.Delete(fullZipToPath);
                            File.Delete(oldFile);
                            File.Move(newFile, oldFile);
                        }

                    }
                }
            }
        }

        public void Update()
        {
            string tmpDir = GetTemporaryDirectory();
            try
            {
                FtpClient ftp = new FtpClient(Settings.FTPServerAddress + ":" + Settings.FTPServerPort, Settings.FTPUser, Settings.FTPPassword);

                string versionsFileLocal = Path.Combine(tmpDir, "versions.txt");

                ftp.download("versions.txt", versionsFileLocal);
                string[] versions = ReadVersionsFile(versionsFileLocal);
                string myVersion = ReadCurrentVersion();
                int myVersionIndex = -1;
                if (myVersion == "")
                {
                    myVersionIndex = 0;
                }
                else
                {
                    for (int i = 0; i < versions.Length; i++)
                    {
                        if (versions[i].Equals(myVersion))
                        {
                            myVersionIndex = i;
                            break;
                        }
                    }
                }

                if (myVersionIndex == -1)
                {
                    throw new Exception("Unknown Version");
                }

                //Update until we have the newest version
                for (int i = myVersionIndex + 1; i < versions.Length; i++)
                {
                    string newVersion = versions[i];
                    Debug.WriteLine("Updating to " + newVersion);
                    string localZipFile = Path.Combine(tmpDir, "update.zip");
                    ftp.download(newVersion + ".zip", localZipFile);
                    InstallUpdateFromZip(localZipFile,Path.GetDirectoryName( Application.ExecutablePath));
                    WriteCurrentVersion(newVersion);
                    File.Delete(localZipFile);
                }
                
            }
            finally
            {
                Directory.Delete(tmpDir, true);
            }
        }

    }
}
