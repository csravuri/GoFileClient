using GoFileClient.Entities;

namespace GoFileClient.Models
{
	public class LogViewViewModel : BaseViewModel
	{
		readonly FileDetailsEntity file;
		public string FileContent { get; set; }
		public LogViewViewModel(INavigation navigation, FileDetailsEntity file) : base(navigation)
		{
			this.file = file;
			if (File.Exists(file.FullPath))
			{
				FileContent = File.ReadAllText(file.FullPath);
			}
		}


	}
}
