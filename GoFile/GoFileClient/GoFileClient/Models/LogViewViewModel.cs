using GoFileClient.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GoFileClient.Models
{
    public class LogViewViewModel : BaseViewModel
    {
        public LogViewViewModel(INavigation navigation, FileDetailsEntity file) : base(navigation)
        {

        }
    }
}
