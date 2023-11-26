using SQLite;

namespace GoFile.Client.Models
{
	public class UploadLine
	{
		[PrimaryKey]
		public Guid Id { get; set; }
		public string FilePath { get; set; }
	}
}
