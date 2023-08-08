using SQLite;

namespace GoFileClient.Entities
{
	public class UploadLineEntity : Entity
	{
		public int UploadHeaderID { get; set; }
		[PrimaryKey, AutoIncrement]
		public int UploadLineID { get; set; }
		public string FileName { get; set; }
		public string FileFullPath { get; set; }
		public string FileSize { get; set; }
		public string URL { get; set; }

		private bool _isUploaded = false;
		public bool IsUploaded
		{
			get
			{
				return _isUploaded;
			}
			set
			{
				SetProperty(ref _isUploaded, value);
			}
		}

		private double _progressPercentage;
		public double ProgressPercentage
		{
			get
			{
				return _progressPercentage;
			}
			set
			{
				SetProperty(ref _progressPercentage, value);
			}
		}

	}
}
