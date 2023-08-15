namespace GoFile.Client.Models
{
    public class BaseModel
    {
        public string token { get; set; }
    }
    public class AccountDetailsModel : BaseModel
    {
        public string id { get; set; }
        public string email { get; set; }
        public string tier { get; set; }
        public string rootFolder { get; set; }
        public int filesCount { get; set; }
        public int totalSize { get; set; }
        public int total30DDLTraffic { get; set; }
        public int credit { get; set; }
        public string currency { get; set; }
        public string currencySign { get; set; }

    }

    public class FolderModel : BaseModel
    {
        public string parentFolderId { get; set; }
    }
}
