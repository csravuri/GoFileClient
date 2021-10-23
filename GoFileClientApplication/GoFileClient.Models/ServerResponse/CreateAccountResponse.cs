namespace GoFileClient.Models
{
    public class CreateAccountResponse : ServerResponseBase
    {
        public Data data { get; set; }

        public class Data
        {
            public string token { get; set; }
        }
    }
}