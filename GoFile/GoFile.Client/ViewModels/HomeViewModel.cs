using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoFile.Client.Models;

namespace GoFile.Client.ViewModels
{
	public partial class HomeViewModel : ObservableObject
	{
		public HomeViewModel()
		{
			//Uploads = new ObservableCollection<UploadHeader>();
		}

		public ObservableCollection<UploadHeader> Uploads { get; } = [];

		[RelayCommand]
		void Load()
		{
			Uploads.Clear();
			// load entries from db
		}

		[RelayCommand]
		void New()
		{
		}

		[RelayCommand]
		void Import()
		{
		}
	}
}
