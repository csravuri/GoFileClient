using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoFile.Client.Database;
using GoFile.Client.Models;
using GoFile.Client.Services;
using GoFile.Client.Utils;

namespace GoFile.Client.ViewModels
{
	public partial class HomeViewModel : ObservableObject
	{
		private readonly GoFileHelper goFileHelper;
		private readonly DbConnection connection;

		public HomeViewModel(GoFileHelper goFileHelper, DbConnection connection)
		{
			this.goFileHelper = goFileHelper;
			this.connection = connection;
			//Uploads = new ObservableCollection<UploadHeader>();
		}

		public ObservableCollection<UploadHeader> Uploads { get; } = [];

		[RelayCommand]
		async Task Load()
		{
			Uploads.Clear();
			var uploads = await connection.GetAll<UploadHeader>(x => true);

			foreach (var upload in uploads)
			{
				Uploads.Add(upload);
			}
		}

		[RelayCommand]
		async Task New()
		{
			var header = new UploadHeader()
			{
				Name = Helper.GetUniqueFolderName()
			};
			goFileHelper.UploadFiles(header);
			await connection.Create(header);
		}

		[RelayCommand]
		void Import()
		{
		}
	}
}
