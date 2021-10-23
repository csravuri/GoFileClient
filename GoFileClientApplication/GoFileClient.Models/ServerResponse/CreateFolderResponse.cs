namespace GoFileClient.Models
{
    public class CreateFolderResponse : ServerResponseBase
    {
        public Data data { get; set; }

        public class Data
        {
            public string id { get; set; }
            public string type { get; set; }
            public string name { get; set; }
            public string parentFolder { get; set; }
            public int createTime { get; set; }
            public object[] childs { get; set; }
            public string code { get; set; }
        }
    }
}