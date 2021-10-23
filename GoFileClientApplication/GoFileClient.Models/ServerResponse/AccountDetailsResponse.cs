namespace GoFileClient.Models
{
    public class AccountDetailsResponse : ServerResponseBase
    {
        public Data data { get; set; }

        public class Data
        {
            public string token { get; set; }
            public string email { get; set; }
            public string tier { get; set; }
            public string rootFolder { get; set; }
            public int filesCount { get; set; }
            public object filesCountLimit { get; set; }
            public int totalSize { get; set; }
            public object totalSizeLimit { get; set; }
            public int total30DDLTraffic { get; set; }
            public object total30DDLTrafficLimit { get; set; }
        }
    }
}