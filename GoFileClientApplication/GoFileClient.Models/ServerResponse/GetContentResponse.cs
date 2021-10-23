using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GoFileClient.Models
{
    public class GetContentResponse : ServerResponseBase
    {
        public Data data { get; set; }

        public class Data
        {
            public bool isOwner { get; set; }
            public string id { get; set; }
            public string type { get; set; }
            public string name { get; set; }
            public string parentFolder { get; set; }
            public string code { get; set; }
            public int createTime { get; set; }
            public bool _public { get; set; }
            public string[] childs { get; set; }
            public int totalDownloadCount { get; set; }
            public int totalSize { get; set; }

            public string contents
            {
                get => _contents;
                set
                {
                    _contents = value;
                    JObject jObject = JObject.Parse(value);
                    actualContents = jObject.Values().Select(j => JsonConvert.DeserializeObject<Content>(j.ToString())).ToArray();
                }
            }

            private string _contents;
            public Content[] actualContents { get; set; }
        }

        public class Content
        {
            public string id { get; set; }
            public string type { get; set; }
            public string name { get; set; }
            public string parentFolder { get; set; }
            public int createTime { get; set; }
            public int size { get; set; }
            public int downloadCount { get; set; }
            public string md5 { get; set; }
            public string mimetype { get; set; }
            public object[] viruses { get; set; }
            public string serverChoosen { get; set; }
            public string directLink { get; set; }
            public string link { get; set; }
        }
    }
}