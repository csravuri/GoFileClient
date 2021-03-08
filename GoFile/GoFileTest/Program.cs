using GoFileHelper.Entites;
using GoFileServiceConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFileTest
{
    class Program
    {
        static void Main(string[] args)
        {
            GoFileConnector goFile = new GoFileConnector();

            BatchDetails batchDetails = null;
            goFile.UploadFile(out batchDetails, @"D:\IOmanager.png", "8iyON9yf7VZZrYZF11N0");

        }
    }
}
