using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUpdater
{
    public static class Settings
    {
        //Server settings
        public static string FTPServerAddress = "localhost";
        public static int FTPServerPort = 21;
        public static string FTPUser = "gameupdate"; //Warning: The FTP User should only have read access.
        public static string FTPPassword = "testpass";
        public static string FTPDirectory = "/";

        //Game settings
        public static string StartFile = "index.html";

    }
}
