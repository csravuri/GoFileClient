using GoFileClient.Models;

namespace GoFileClient.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPage : ContentPage
	{
		private readonly AboutViewModel viewModel;
		public AboutPage()
		{
			InitializeComponent();
			BindingContext = this.viewModel = new AboutViewModel(this.Navigation);
		}
	}
}