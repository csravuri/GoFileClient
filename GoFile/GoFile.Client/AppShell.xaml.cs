using GoFile.Client.Views;

namespace GoFile.Client
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			Routing.RegisterRoute(nameof(UploadFilesPage), typeof(UploadFilesPage));
		}
	}
}
