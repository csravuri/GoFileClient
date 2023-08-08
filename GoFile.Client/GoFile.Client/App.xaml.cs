﻿namespace GoFile.Client;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

	//#region Custom Properties // cant store complex types not usefull now

	//private static string StorageLocationPathKey = "StorageLocationPath";
	//public static string StorageLocationPath
	//{
	//    get
	//    {
	//        if (Current.Properties.ContainsKey(StorageLocationPathKey)
	//            && Current.Properties[StorageLocationPathKey] is string locationPath
	//            && !string.IsNullOrWhiteSpace(locationPath))
	//        {
	//            return locationPath;
	//        }
	//        else
	//        {
	//            locationPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
	//            Current.Properties[StorageLocationPathKey] = locationPath;
	//            return Current.Properties[StorageLocationPathKey] as string;
	//        }
	//    }

	//    // setter for choise by user to take any path. 
	//    // TODO
	//}

	////private static string UploadHeadersKey = "UploadHeaders";

	////public static List<UploadHeaderEntity> UploadHeaders
	////{
	////    get
	////    {
	////        if (Current.Properties.ContainsKey(UploadHeadersKey)
	////            && Current.Properties[UploadHeadersKey] is List<UploadHeaderEntity> headers
	////            && headers != null)
	////        {
	////            return headers;
	////        }
	////        else
	////        {
	////            headers = new List<UploadHeaderEntity>();
	////            Current.Properties.Add(UploadHeadersKey, headers);
	////            return headers;
	////        }
	////    }
	////}

	//#endregion

	//public App()
	//{
	//    InitializeComponent();

	//    MainPage = new NavigationPage(new Views.LoadingPage()) 
	//    { 
	//        BarBackgroundColor = Color.FromHex("#570099")
	//    };
	//}
}
