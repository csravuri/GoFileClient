namespace GoFileClient.Models
{
    public class UploadFileResponse : ServerResponseBase
    {
        public Data data { get; set; }

        public class Data
        {
            public string downloadPage { get; set; }
            public string code { get; set; }
            public string parentFolder { get; set; }
            public string fileId { get; set; }
            public string fileName { get; set; }
            public string md5 { get; set; }
            public string directLink { get; set; }
            public string info { get; set; }
        }
    }
}