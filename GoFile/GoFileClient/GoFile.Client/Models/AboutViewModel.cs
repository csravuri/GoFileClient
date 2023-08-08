using GoFileClient.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

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
