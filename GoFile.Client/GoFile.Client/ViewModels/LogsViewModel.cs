using System.Collections.ObjectModel;
using System.Windows.Input;
using GoFileClient.Entities;
using GoFileClient.Views;
using GoFileHelper.Common;

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
