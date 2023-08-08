using GoFileClient.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace GoFileClient.Models
{
    public class LogViewViewModel : BaseViewModel
    {
        FileDetailsEntity file;
        public string FileContent { get; set; }
        public LogViewViewModel(INavigation navigation, FileDetailsEntity file) : base(navigation)
        {
            this.file = file;
            if (File.Exists(file.FullPath))
            {
                FileContent = File.ReadAllText(file.FullPath);
            }
        }


    }
}
