using GoFileClient.Models;

namespace GoFileClient.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadingPage : ContentPage
	{
		public LoadingPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			LoadingViewModel viewModel = new LoadingViewModel(this.Navigation, this);
			viewModel.LoadLoadingCommand.Execute(null);
		}
	}
}