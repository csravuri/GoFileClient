using GoFileClient.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Essentials;
using System.IO;
using GoFileClient.Common;
using GoFileHelper.Common;
using System.ComponentModel;

namespace GoFileClient.Models
{
    public class UploadDetailsViewModel : BaseViewModel//, INotifyPropertyChanged
    {
        
        public UploadHeaderEntity UploadHeader { get; private set; }
        public ObservableCollection<UploadLineEntity> UploadLines { get; set; }

        public ICommand LoadUploadDetailsCommand { get; private set; }
        public ICommand AddLineCommand { get; private set; }
        public ICommand CopyClickCommand { get; private set; }
        public ICommand DeleteLineCommand { get; private set; }
        public ICommand DeleteLineCloudCommand { get; private set; }
        public ICommand DeleteLineLocalCommand { get; private set; }
        public ICommand DeleteLineBothCommand { get; private set; }
        //public ICommand CopyClickAndHoldCommand { get; private set; }

        private bool isFilePickerActive = false;

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        public UploadDetailsViewModel(INavigation navigation, UploadHeaderEntity uploadHeader) : base(navigation)
        {
            this.UploadHeader = uploadHeader;
            UploadLines = new ObservableCollection<UploadLineEntity>();
            LoadUploadDetailsCommand = new Command(async () => await ExecuteLoadUploadDetailsCommand());
            AddLineCommand = new Command(async () => await ExecuteAddLineCommand());
            CopyClickCommand = new Command<string>(async (string Text) => await ExecuteCopyClickCommand(Text));
            DeleteLineCommand = new Command<UploadLineEntity>(async (UploadLineEntity UploadLine) => await ExecuteDeleteLineCommand(UploadLine));
            DeleteLineCloudCommand = new Command<UploadLineEntity>(async (UploadLineEntity UploadLine) => await ExecuteDeleteLineCloudCommand(UploadLine));
            DeleteLineLocalCommand = new Command<UploadLineEntity>(async (UploadLineEntity UploadLine) => await ExecuteDeleteLineLocalCommand(UploadLine));
            DeleteLineBothCommand = new Command<UploadLineEntity>(async (UploadLineEntity UploadLine) => await ExecuteDeleteLineBothCommand(UploadLine));
            //CopyClickAndHoldCommand = new Command(async () => await ExecuteCopyClickAndHoldCommand());
        }

        private async Task ExecuteDeleteLineBothCommand(UploadLineEntity uploadLine)
        {
            ToastMessage.ShowShortAlert($"{uploadLine.FileName} deleted from Server and Local.");
        }

        private async Task ExecuteDeleteLineLocalCommand(UploadLineEntity uploadLine)
        {
            ToastMessage.ShowShortAlert($"{uploadLine.FileName} deleted from Local.");
        }

        private async Task ExecuteDeleteLineCloudCommand(UploadLineEntity uploadLine)
        {
            ToastMessage.ShowShortAlert($"{uploadLine.FileName} deleted from Server.");
        }

        private async Task ExecuteDeleteLineCommand(UploadLineEntity uploadLine)
        {
            UploadLines.Remove(uploadLine);
            UploadHeader.LineCount--;
            await DbConnection.UpdateRecord(UploadHeader);
            await DbConnection.DeleteRecord(uploadLine);
            File.Delete(uploadLine.FileFullPath);
            ToastMessage.ShowShortAlert($"{uploadLine.FileName} deleted.");
        }

        //private async Task ExecuteCopyClickAndHoldCommand()
        //{
        //    ToastMessage.ShowLongAlert("show URL in text box to compy");
        //}

        private async Task ExecuteCopyClickCommand(string text)
        {
            await Clipboard.SetTextAsync(text);
            ToastMessage.ShowLongAlert("Text copied to clipboard");
        }

        private async Task ExecuteAddLineCommand()
        {
            isFilePickerActive = true;
            PickOptions options = new PickOptions()
            {
                PickerTitle = "Select Files"
            };

            var files = await FilePicker.PickMultipleAsync(options);

            if (files == null || files.Count() == 0)
                return;

            List<UploadLineEntity> lines = new List<UploadLineEntity>();
            foreach (FileResult file in files)
            {
                FileInfo info = new FileInfo(file.FullPath);
                info.CopyTo(UploadHeader.LocalFolderPath, true);
                // TODO: File Rename if confilict

                lines.Add(new UploadLineEntity()
                {
                    UploadHeaderID = this.UploadHeader.UploadHeaderID,
                    FileName = file.FileName,
                    FileFullPath = Path.Combine(UploadHeader.LocalFolderPath, file.FileName),
                    FileSize = Utils.HumanReadableFileSize(info.Length)
                });
            }

            UploadHeader.LineCount += lines.Count;

            await DbConnection.InsertRecord(lines);
            await DbConnection.UpdateRecord(UploadHeader);
            lines.ForEach(x => UploadLines.Add(x));
            isFilePickerActive = false;

            GoFileUploadManager uploadManager = GoFileUploadManager.GetManager(UploadHeader);
            Task uploadTask = new Task(async () => await uploadManager.StartUploadQueue());
            uploadTask.Start();
            ToastMessage.ShowShortAlert("File(s) added to Queue.");
        }

        private async Task ExecuteLoadUploadDetailsCommand()
        {
            if (!isFilePickerActive)
            {
                UploadLines.Clear();
                List<UploadLineEntity> lines = await DbConnection.GetUploadLines();
                lines.Where(x => x.UploadHeaderID == this.UploadHeader.UploadHeaderID).ToList().ForEach(x => UploadLines.Add(x));
            }
        }

        protected override void NetworkConnected()
        {
            base.NetworkConnected();
            GoFileUploadManager uploadManager = GoFileUploadManager.GetManager(UploadHeader);
            Task uploadTask = new Task(async () => await uploadManager.StartUploadQueue());
            uploadTask.Start();
        }
    }
}
