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
    public partial class LogViewPage : ContentPage
    {
        private LogViewViewModel viewModel;

        public LogViewPage(LogViewViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
    }
}