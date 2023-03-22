using System;
using System.IO;

namespace GoFileHelper.Common
{
    public static class Logger
    {
        private static readonly string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Log_Files");

        public static void Log(string message)
        {
            Utils.MakeSureDirectoryExists(folderPath);

            string fileName = $"GoFileLog_{DateTime.Now.ToString("yyyy_MMM_dd")}.txt";

            string fileFullPath = Path.Combine(folderPath, fileName);

            File.AppendAllText(fileFullPath, $"{DateTime.Now.ToString("yyyy/MMM/dd HH:mm:ss.fff")}: {message}\n");
        }

        public static FileInfo[] GetLogFiles()
        {
            Utils.MakeSureDirectoryExists(folderPath);

            DirectoryInfo info = new DirectoryInfo(folderPath);

            return info.GetFiles();
        }
    }
}