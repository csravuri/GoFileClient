using SQLite;

namespace GoFile.Client.Models
{
	public class UploadHeader
	{
		[PrimaryKey]
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Name { get; set; }
		public string Token { get; set; }
		public string CloudRootFolder { get; set; }
		public string Email { get; set; }
	}
}
