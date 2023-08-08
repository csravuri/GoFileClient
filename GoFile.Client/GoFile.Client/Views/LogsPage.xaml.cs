using GoFileClient.Models;

namespace GoFileClient.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogsPage : ContentPage
	{
		private readonly LogsViewModel viewModel;
		public LogsPage()
		{
			InitializeComponent();
			BindingContext = this.viewModel = new LogsViewModel(this.Navigation);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			viewModel.LoadLogFilesCommand.Execute(null);
		}

		private void FileInfo_Tapped(object sender, ItemTappedEventArgs e)
		{
			viewModel.DisplayLogCommand.Execute(e.Item);
		}
	}
}