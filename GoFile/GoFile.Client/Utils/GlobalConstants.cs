namespace GoFile.Client.Utils
{
	public static class GlobalConstants
	{
		public static string RootFolder => FileSystem.AppDataDirectory;
	}

	public static class Helper
	{
		internal static string GetUniqueFolderName()
		{
			return Guid.NewGuid().ToString().Replace("-", "")[..5];
		}
	}
}
