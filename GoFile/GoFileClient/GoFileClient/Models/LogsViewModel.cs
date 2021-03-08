using GoFileClient.Entities;
using GoFileClient.Views;
using GoFileHelper.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoFileClient.Models
{
    public class LogsViewModel : BaseViewModel
    {
        public ObservableCollection<FileDetailsEntity> FileInfos { get; set; }
        public ICommand DisplayLogCommand { get; private set; }
        public ICommand LoadLogFilesCommand { get; private set; }
        public LogsViewModel(INavigation navigation) : base(navigation)
        {
            FileInfos = new ObservableCollection<FileDetailsEntity>();
            DisplayLogCommand = new Command<FileDetailsEntity>(async (FileDetailsEntity file) => await ExecuteDisplayLogCommand(file));
            LoadLogFilesCommand = new Command(async () => await ExecuteLoadLogFilesCommand());
        }

        private async Task ExecuteLoadLogFilesCommand()
        {
            FileInfos.Clear();
            FileInfo[] files = Logger.GetLogFiles();
            FileInfos.AddRange(files.Select(x => new FileDetailsEntity() 
            {
                FileName = x.Name,
                FullPath = x.FullName,
                Size = Utils.HumanReadableFileSize(x.Length),
                CreatedDate = x.CreationTime,
                ModifiedDate = x.LastWriteTime,
                OriginalFileInfo = x
            }));            
        }

        private async Task ExecuteDisplayLogCommand(FileDetailsEntity fileDetail)
        {
            await Navigation.PushAsync(new LogViewPage(new LogViewViewModel(this.Navigation, fileDetail)));
        }
    }
}
