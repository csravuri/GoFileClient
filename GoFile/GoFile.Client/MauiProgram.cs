﻿using GoFile.Client.Database;
using GoFile.Client.Services;
using GoFile.Client.ViewModels;
using GoFile.Client.Views;
using Microsoft.Extensions.Logging;

namespace GoFile.Client
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

			builder.Services.AddSingleton(typeof(DbConnection));
			builder.Services.AddSingleton(typeof(GoFileHelper));
			builder.Services.AddSingleton(typeof(GoFileWrapper));

			builder.Services.AddSingleton(typeof(HomePage));
			builder.Services.AddSingleton(typeof(HomeViewModel));

			builder.Services.AddTransient(typeof(UploadFilesPage));
			builder.Services.AddTransient(typeof(UploadFilesViewModel));

#if DEBUG
			builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
	}
}
