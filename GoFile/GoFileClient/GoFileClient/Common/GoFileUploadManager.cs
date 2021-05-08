using GoFileClient.Database;
using GoFileClient.Entities;
using GoFileHelper.Entites;
using GoFileServiceConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GoFileClient.Common
{
    public class GoFileUploadManager
    {
        private static Dictionary<int, GoFileUploadManager> AllManagers = new Dictionary<int, GoFileUploadManager>();
        private bool isBusy = false;
        private UploadHeaderEntity header;
        private DbConnection DBConnection = DbConnection.GetDbConnection();
        private GoFileUploadManager(UploadHeaderEntity header)
        {
            this.header = header;
        }

        public static GoFileUploadManager GetManager(UploadHeaderEntity header)
        {
            if (!AllManagers.ContainsKey(header.UploadHeaderID))
            {
                AllManagers.Add(header.UploadHeaderID, new GoFileUploadManager(header));
            }

            return AllManagers[header.UploadHeaderID];
        }

        public async Task StartUploadQueue()
        {
            if (isBusy)
                return;

            isBusy = true;

            GoFileConnector connector = new GoFileConnector();
            List<UploadLineEntity> lines = GetPendingLines(await DBConnection.GetUploadLines());

            while (lines.Count > 0)
            {
                if (await UploadFiles(lines, connector))
                {
                    lines = GetPendingLines(await DBConnection.GetUploadLines());
                }
                else
                {
                    Device.InvokeOnMainThreadAsync(() => ToastMessage.ShowLongAlert("Issue with Internet. Check logs for more info."));                    
                    break;
                }
            }

            isBusy = false;
        }

        private async Task<bool> UploadFiles(List<UploadLineEntity> lines, GoFileConnector connector)
        {
            foreach (UploadLineEntity line in lines)
            {
                if (string.IsNullOrWhiteSpace(header.AdminCode))
                {
                    BatchDetails batchDetails;
                    if (connector.UploadFile(out batchDetails, line.FileFullPath, callback: (args) => line.ProgressPercentage = args.ProgressPercentage / 100.0))
                    {
                        header.AdminCode = batchDetails.adminCode;
                        header.Code = batchDetails.code;
                        header.URL = $"https://gofile.io/d/{batchDetails.code}";
                        line.IsUploaded = true;

                        await DBConnection.UpdateRecord(header);
                        await DBConnection.UpdateRecord(line);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    BatchDetails batchDetails;
                    if (connector.UploadFile(out batchDetails, line.FileFullPath, header.AdminCode, callback: (args) => line.ProgressPercentage = args.ProgressPercentage / 100.0))
                    {
                        line.IsUploaded = true;

                        await DBConnection.UpdateRecord(header);
                        await DBConnection.UpdateRecord(line);
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private List<UploadLineEntity> GetPendingLines(List<UploadLineEntity> lines)
        {
            return lines.Where(x => x.UploadHeaderID == header.UploadHeaderID && !x.IsUploaded).ToList();
        }
    }
}
