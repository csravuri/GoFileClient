using GoFile.Client.Models;

namespace GoFile.Client.Services
{
	public class GoFileHelper
	{
		private readonly GoFileWrapper wrapper;

		public GoFileHelper(GoFileWrapper wrapper)
		{
			this.wrapper = wrapper;
		}

		public void UploadFiles(UploadHeader header, UploadLine[] lines = null, Action<UploadHeader, UploadLine> statusUpdateAction = null)
		{
			if (header is null)
			{
				return;
			}

			if (string.IsNullOrWhiteSpace(header.Token))
			{
				header.Token = wrapper.CreateAccount();
			}

			if (string.IsNullOrWhiteSpace(header.CloudRootFolder))
			{
				var accountDetails = wrapper.GetAccountDetails(header.Token);
				if (accountDetails is null)
				{
					return;
				}

				header.CloudRootFolder = accountDetails.rootFolder;
				header.Email = accountDetails.email;
			}

			if (lines is null
				|| lines.Length == 0)
			{
				return;
			}
		}
	}
}
