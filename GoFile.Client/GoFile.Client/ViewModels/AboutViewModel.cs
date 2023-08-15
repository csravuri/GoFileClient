using System.Windows.Input;
using GoFileClient.Views;

namespace GoFileClient.Models
{
	public class AboutViewModel : BaseViewModel
	{
		public ICommand ViewLogsClickedCommand { get; private set; }
		public AboutViewModel(INavigation navigation) : base(navigation)
		{
			ViewLogsClickedCommand = new Command(async () => await ExecuteViewLogsClickedCommand());
		}

		private async Task ExecuteViewLogsClickedCommand()
		{
			await Navigation.PushAsync(new LogsPage());
		}
	}
}
