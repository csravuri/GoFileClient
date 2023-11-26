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
    public partial class BatchDetailsPage : ContentPage
    {
        BatchDetailsViewModel viewModel;
        public BatchDetailsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new BatchDetailsViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadBatchDetailsCommand.Execute(null);
        }

        private void UploadHeader_Tapped(object sender, ItemTappedEventArgs e)
        {
            viewModel.NavigateUploadDetailsCommand.Execute(e.Item);
        }

        private void DeleteBatch_Clicked(object sender, EventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            viewModel.DeleteBatchCommand.Execute(menu.CommandParameter);
        }

        private void ShowFiles_Clicked(object sender, EventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            viewModel.ShowFilesCommand.Execute(menu.CommandParameter);
        }
    }
}