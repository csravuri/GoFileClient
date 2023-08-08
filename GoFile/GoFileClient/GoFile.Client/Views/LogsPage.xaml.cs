using GoFileClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

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