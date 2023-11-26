using GoFile.Client.ViewModels;

namespace GoFile.Client.Views;

public partial class UploadFilesPage : ContentPage
{
	public UploadFilesPage(UploadFilesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}