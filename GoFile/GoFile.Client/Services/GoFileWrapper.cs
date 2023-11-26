namespace GoFile.Client.Services
{
	public class GoFileWrapper
	{
		public string CreateAccount()
		{
			return "";
		}

		public AccountDetails GetAccountDetails(string token)
		{
			return null;
		}



		const string ApiBaseUrl = "https://api.gofile.io";
	}


	public class AccountDetails
	{
		public string id { get; set; }
		public string email { get; set; }
		public string tier { get; set; }
		public string token { get; set; }
		public string rootFolder { get; set; }
		public int filesCount { get; set; }
		public int totalSize { get; set; }
		public int total30DDLTraffic { get; set; }
		public int credit { get; set; }
		public string currency { get; set; }
		public string currencySign { get; set; }
	}

}
