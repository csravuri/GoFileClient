namespace GoFileClient
{
	public partial class App : Application
	{
		#region Custom Properties // cant store complex types not usefull now

		private static string StorageLocationPathKey = "StorageLocationPath";
		public static string StorageLocationPath
		{
			get
			{
				if (Microsoft.Maui.Storage.Preferences.ContainsKey(StorageLocationPathKey)
					&& Microsoft.Maui.Storage.Preferences.Get(StorageLocationPathKey, "") is string locationPath
					&& !string.IsNullOrWhiteSpace(locationPath))
				{
					return locationPath;
				}
				else
				{
					locationPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
					Microsoft.Maui.Storage.Preferences.Set(StorageLocationPathKey, locationPath);
					return locationPath;
				}
			}

			// setter for choise by user to take any path. 
			// TODO
		}

		//private static string UploadHeadersKey = "UploadHeaders";

		//public static List<UploadHeaderEntity> UploadHeaders
		//{
		//    get
		//    {
		//        if (Microsoft.Maui.Storage.Preferences.ContainsKey(UploadHeadersKey)
		//            && Microsoft.Maui.Storage.Preferences[UploadHeadersKey] is List<UploadHeaderEntity> headers
		//            && headers != null)
		//        {
		//            return headers;
		//        }
		//        else
		//        {
		//            headers = new List<UploadHeaderEntity>();
		//            Microsoft.Maui.Storage.Preferences.Add(UploadHeadersKey, headers);
		//            return headers;
		//        }
		//    }
		//}

		#endregion

		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new Views.LoadingPage())
			{
				BarBackgroundColor = Color.FromArgb("#570099")
			};
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
