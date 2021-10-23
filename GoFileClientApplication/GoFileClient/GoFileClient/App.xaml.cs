using GoFileClient.ServiceConnector;
using Xamarin.Forms;

namespace GoFileClient
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<Logger.ILogger, Logger.Logger>();
            DependencyService.Register<IGoFileServiceConnector, GoFileServiceConnector>();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}