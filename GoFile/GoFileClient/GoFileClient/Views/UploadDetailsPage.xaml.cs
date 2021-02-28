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
    public partial class UploadDetailsPage : ContentPage
    {
        private UploadDetailsViewModel viewModel;
                
        public UploadDetailsPage(UploadDetailsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadUploadDetailsCommand.Execute(null);
        }

        private void DeleteLine_Clicked(object sender, EventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            viewModel.DeleteLineCommand.Execute(menu.CommandParameter);
        }
    }
}