using GoFileClient.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoFileClient.Models
{
    public class LoadingViewModel : BaseViewModel
    {
        public ICommand LoadLoadingCommand { get; private set; }
        private Page page;
        public LoadingViewModel(INavigation navigation, Page page) : base(navigation)
        {
            this.page = page;
            LoadLoadingCommand = new Command(async () => await ExecuteLoadLoadingCommand());
        }

        private async Task ExecuteLoadLoadingCommand()
        {
            if (Database.DbConnection.IsLoadingComplete())
            {
                await Navigation.PushAsync(new BatchDetailsPage());
                Navigation.RemovePage(page);
            }
            else
            {
                Database.DbConnection.LoadingComplete += OnLoadingComplete;
                Database.DbConnection.GetDbConnection();
            }
        }

        private async void OnLoadingComplete(object sender, EventArgs e)
        {            
            await Navigation.PushAsync(new BatchDetailsPage());
            Navigation.RemovePage(page);
        }
    }
}
