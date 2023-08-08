using GoFileClient.Models;

namespace GoFileClient.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogViewPage : ContentPage
	{
		private readonly LogViewViewModel viewModel;

		public LogViewPage(LogViewViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = this.viewModel = viewModel;
		}
	}
}