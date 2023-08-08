using System.Windows.Input;
using GoFileClient.Common;
using GoFileClient.Database;

namespace GoFileClient.Models
{
	public class BaseViewModel
	{
		protected INavigation Navigation { get; set; }
		protected DbConnection DbConnection { get; private set; } = DbConnection.GetDbConnection();
		public ICommand LinkClickedCommand { get; private set; }

		public BaseViewModel(INavigation navigation)
		{
			this.Navigation = navigation;
			LinkClickedCommand = new Command<string>(async (string URL) => await ExecuteLinkClickedCommand(URL));
			Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
		}

		private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
		{
			if (e.NetworkAccess == NetworkAccess.Internet)
			{
				NetworkConnected();
			}
		}

		protected virtual void NetworkConnected()
		{
			ToastMessage.ShowShortAlert("Connected to Internet.");
		}

		private async Task ExecuteLinkClickedCommand(string URL)
		{
			await Launcher.TryOpenAsync(URL);
		}

		//protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
		//{
		//    if (EqualityComparer<T>.Default.Equals(backingStore, value))
		//        return false;

		//    backingStore = value;
		//    onChanged?.Invoke();
		//    OnPropertyChanged(propertyName);
		//    return true;
		//}

		//public event PropertyChangedEventHandler PropertyChanged;

		//protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
		//{
		//    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		//}

	}
}
