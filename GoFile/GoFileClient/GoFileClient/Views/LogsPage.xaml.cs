using GoFileClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoFileClient.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogsPage : ContentPage
    {
        private LogsViewModel viewModel;
        public LogsPage()
        {
            InitializeComponent();
            BindingContext = this.viewModel = new LogsViewModel(this.Navigation);
        }
    }
}