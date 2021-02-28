using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

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
        public bool IsUploaded { get; set; } = false;
    }
}
