using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GoFileClient.Entities
{
    public class UploadHeaderEntity : Entity
    {
        [PrimaryKey, AutoIncrement]
        public int UploadHeaderID { get; set; }

        private string _code;
        public string Code 
        { 
            get 
            {
                return _code;
            }
            set 
            {
                SetProperty(ref _code, value);                            
            } 
        }

        private string _adminCode;
        public string AdminCode
        {
            get
            {
                return _adminCode;
            }
            set
            {
                SetProperty(ref _adminCode, value);
            }
        }

        private string _uRL;
        public string URL 
        {
            get
            {
                return _uRL;
            }
            set
            {
                SetProperty(ref _uRL, value);
            }
        }
        public string LocalFolderName { get; set; }
        public string LocalFolderPath { get; set; }
        public int LineCount { get; set; }

        
     

    }
}
