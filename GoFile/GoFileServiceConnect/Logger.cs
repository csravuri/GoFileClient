using GoFileHelper.Common;
using System;
using System.IO;

namespace GoFileServiceConnect
{
    public static class Logger
    {
        public static void Log(string message)
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Log_Files");
            Utils.MakeSureDirectoryExists(folderPath);

            string fileName = $"GoFileLog_{DateTime.Now.ToString("yyyy_MMM_dd")}.txt";

            string fileFullPath = Path.Combine(folderPath, fileName);

            File.AppendAllText(fileFullPath, $"{message}\n");
        }
    }
}
