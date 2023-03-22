using GoFileHelper.Entites;
using GoFileServiceConnect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            goFile.UploadFile(out BatchDetails batchDetails, @"D:\test.txt", null, (x) => Debug.Print($"{DateTime.Now.ToString()}, Tatal: {x.TotalBytes}, Trasfered: {x.BytesTransferred}, percentage:{x.ProgressPercentage}"));
            Debug.Print($"{batchDetails.code}, {batchDetails.adminCode}");

            //goFile.GetUpload("oNNorW", "L8Tzat3m4PYteCIjY9Lr");
        }
    }
}
