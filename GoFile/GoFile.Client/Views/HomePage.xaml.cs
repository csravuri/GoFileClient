using GoFile.Client.ViewModels;

namespace GoFile.Client.Views;

public partial class HomePage : ContentPage
{
	private readonly HomeViewModel vm;

	public HomePage(HomeViewModel vm)
	{
		InitializeComponent();
		this.vm = vm;
		BindingContext = vm;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		vm.LoadCommand.Execute(null);
	}
}