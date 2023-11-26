using System.Text.Json;

namespace GoFile.Client.Services
{
	public class GoFileWrapper
	{
		public async Task<string> CreateAccount()
		{
			var tokenDetails = await CallHttpService<TokenDetails>(async f => await f.GetAsync("createAccount"));
			return tokenDetails?.token;
		}

		public AccountDetails GetAccountDetails(string token)
		{
			return null;
		}

		async Task<T> CallHttpService<T>(Func<HttpClient, Task<HttpResponseMessage>> httpVerbFunc) where T : class
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(ApiBaseUrl);
				var httpResponse = await httpVerbFunc(client);

				if (httpResponse.IsSuccessStatusCode)
				{
					var httpResponseString = await httpResponse.Content.ReadAsStringAsync();
					var responseObject = JsonSerializer.Deserialize<ServiceResponse<T>>(httpResponseString);

					if (responseObject != null
						&& responseObject.status == Ok)
					{
						return responseObject.data;
					}
				}
			}
			return null;
		}

		const string ApiBaseUrl = "https://api.gofile.io";
		const string Ok = "ok";
	}
}
