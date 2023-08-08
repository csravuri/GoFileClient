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

        private void File_Tapped(object sender, ItemTappedEventArgs e)
        {
            ListView listView = sender as ListView;
            listView.SelectedItem = null;
        }

        private void DeleteLine_CloudClicked(object sender, EventArgs e)
        {
            ImageButton button = sender as ImageButton;
            viewModel.DeleteLineCloudCommand.Execute(button.CommandParameter);
        }

        private void DeleteLine_BothClicked(object sender, EventArgs e)
        {
            ImageButton button = sender as ImageButton;
            viewModel.DeleteLineBothCommand.Execute(button.CommandParameter);
        }

        private void DeleteLine_LocalClicked(object sender, EventArgs e)
        {
            ImageButton button = sender as ImageButton;
            viewModel.DeleteLineLocalCommand.Execute(button.CommandParameter);
        }
    }
}