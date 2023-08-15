using System.Collections.ObjectModel;
using System.Windows.Input;
using GoFileClient.Common;
using GoFileClient.Entities;
using GoFileClient.Views;

namespace GoFileClient.Models
{
	public class BatchDetailsViewModel : BaseViewModel
	{
		public ICommand LoadBatchDetailsCommand { get; private set; }
		public ICommand NavigateUploadDetailsCommand { get; private set; }
		public ICommand ShowFilesCommand { get; private set; }
		public ICommand DeleteBatchCommand { get; private set; }
		public ICommand AboutClickedCommand { get; private set; }
		public ICommand AddURLCommand { get; private set; }

		public ObservableCollection<UploadHeaderEntity> UploadHeaders { get; set; }

		public BatchDetailsViewModel(INavigation navigation) : base(navigation)
		{
			UploadHeaders = new ObservableCollection<UploadHeaderEntity>();
			LoadBatchDetailsCommand = new Command(async () => await ExecuteLoadBatchDetailsCommand());
			NavigateUploadDetailsCommand = new Command<UploadHeaderEntity>(async (UploadHeaderEntity uploadHeader) => await ExecuteNavigateUploadDetailsCommand(uploadHeader));
			ShowFilesCommand = new Command<UploadHeaderEntity>(async (UploadHeaderEntity uploadHeader) => await ExecuteShowFilesCommand(uploadHeader));
			DeleteBatchCommand = new Command<UploadHeaderEntity>(async (UploadHeaderEntity uploadHeader) => await ExecuteDeleteBatchCommand(uploadHeader));
			AboutClickedCommand = new Command(async () => await ExecuteAboutClickedCommand());
			AddURLCommand = new Command(async () => await ExecuteAddURLCommand());
		}

		private async Task ExecuteAddURLCommand()
		{
			ToastMessage.ShowLongAlert("Coming soon!");
		}

		private async Task ExecuteAboutClickedCommand()
		{
			await Navigation.PushAsync(new AboutPage());
		}

		private async Task ExecuteDeleteBatchCommand(UploadHeaderEntity uploadHeader)
		{
			UploadHeaders.Remove(uploadHeader);
			await DbConnection.DeleteRecord(uploadHeader);
			Directory.Delete(uploadHeader.LocalFolderPath, true);
			ToastMessage.ShowShortAlert($"{uploadHeader.LocalFolderName} deleted.");
		}

		private async Task ExecuteShowFilesCommand(UploadHeaderEntity uploadHeader)
		{
			ToastMessage.ShowShortAlert("want to show files but how?.");
		}

		private async Task ExecuteNavigateUploadDetailsCommand(UploadHeaderEntity UploadHeader)
		{
			if (UploadHeader == null)
			{
				UploadHeader = await CreateLocalBatch();
				ToastMessage.ShowLongAlert("New Batch created.");
			}

			await Navigation.PushAsync(new UploadDetailsPage(new UploadDetailsViewModel(Navigation, UploadHeader)));
		}

		private async Task<UploadHeaderEntity> CreateLocalBatch()
		{
			List<UploadHeaderEntity> headers = await DbConnection.GetUploadHeaders();
			// Random Folder Name to move files
			string folderName = null;

			do
			{
				folderName = RandomTextGenerator.GenerateRandomText(RandomTextGenerator.TextType.Alpha, 10);
			}
			while (headers.Select(x => x.LocalFolderName).Contains(folderName));

			UploadHeaderEntity uploadHeaderEntity = new UploadHeaderEntity()
			{
				LocalFolderName = folderName,
				//LocalFolderPath = Directory.CreateDirectory(Path.Combine(App.StorageLocationPath, folderName)).FullName,
				CreatedDate = DateTime.Now
			};


			await DbConnection.InsertRecord(uploadHeaderEntity);

			return uploadHeaderEntity;
		}

		private async Task ExecuteLoadBatchDetailsCommand()
		{
			UploadHeaders.Clear();

			List<UploadHeaderEntity> headers = await DbConnection.GetUploadHeaders();

			headers.ForEach(x => UploadHeaders.Add(x));

			Task t = new Task(UploadPendingFiles);
			t.Start();
		}

		private async void UploadPendingFiles()
		{
			List<UploadHeaderEntity> headers = await DbConnection.GetUploadHeaders();
			foreach (UploadHeaderEntity header in headers)
			{
				GoFileUploadManager uploadManager = GoFileUploadManager.GetManager(header);
				await uploadManager.StartUploadQueue();
			}
		}

		protected override void NetworkConnected()
		{
			base.NetworkConnected();
			Task t = new Task(UploadPendingFiles);
			t.Start();
		}
	}
}
