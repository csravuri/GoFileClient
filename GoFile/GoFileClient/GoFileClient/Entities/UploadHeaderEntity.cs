using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoFileClient.Entities
{
    public class UploadHeaderEntity : Entity
    {
        [PrimaryKey, AutoIncrement]
        public int UploadHeaderID { get; set; }
        public string Code { get; set; }
        public string AdminCode { get; set; }
        public string URL 
        { 
            get 
            {
                if (!string.IsNullOrWhiteSpace(Code))
                {
                    return $"https://gofile.io/d/{Code}";
                }
                else
                {
                    return "";
                }
            } 
            set 
            {

            } 
        }
        public string LocalFolderName { get; set; }
        public string LocalFolderPath { get; set; }
        public int LineCount { get; set; }
        
    }
}
