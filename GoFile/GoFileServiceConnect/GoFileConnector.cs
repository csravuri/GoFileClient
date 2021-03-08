using GoFileHelper.Entites;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GoFileServiceConnect
{
    public class GoFileConnector
    {
        /// <summary>
        /// Returns false if server details not found
        /// </summary>
        /// <param name="serverAddress"></param>
        /// <returns></returns>
        private bool GetServers(out string serverAddress)
        {
            string URL = "https://apiv2.gofile.io";
            serverAddress = null;
                        
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                //HTTP GET
                var responseTask = client.GetAsync("getServer");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var serverDetailResponse = readTask.Result;
                    Logger.Log(serverDetailResponse);

                    serverAddress = JsonConvert.DeserializeObject<ServerDetailResponse>(serverDetailResponse).data.server;

                    return true;
                }
                else //web api sent error response 
                {
                    Logger.Log($"Something went wrong while getting active server. API {URL} is not reachable.");
                    return false;
                }
            }
        }

        /// <summary>
        /// Return false if file upload not success
        /// </summary>
        /// <param name="fileFullPath">If file doesnot exist nothing will happen</param>
        /// <param name="adminCode">If admin code is not provided, will create new batch</param>
        public bool UploadFile(out BatchDetails batchDetails, string fileFullPath, string adminCode = null)
        {
            batchDetails = null;
            bool uploadSuccess = false;
            if (fileFullPath == null || !File.Exists(fileFullPath))
                return false;

            if (!GetServers(out string server) || string.IsNullOrWhiteSpace(server))
            {
                return false;
            }
            
            MultipartFormDataContent formDataContent = new MultipartFormDataContent();
            
            string URL = $"https://{server}.gofile.io";

            FileStream fileStream = new FileStream(fileFullPath, FileMode.Open);
            HttpContent fileContent = new StreamContent(fileStream);

            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = fileStream.Name
            };
            formDataContent.Add(fileContent, "file");

            if (!string.IsNullOrWhiteSpace(adminCode))
            {
                HttpContent adminCodeContent = new StringContent(adminCode);

                adminCodeContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "ac"
                };
                formDataContent.Add(adminCodeContent, "ac");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                //HTTP POST
                var responseTask = client.PostAsync("uploadFile", formDataContent);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var uploadResponse = readTask.Result;
                    Logger.Log(uploadResponse);

                    if (!string.IsNullOrWhiteSpace(adminCode))
                    {
                        UploadServerResponse response = JsonConvert.DeserializeObject<UploadServerResponse>(uploadResponse);
                        batchDetails = new BatchDetails()
                        {
                            adminCode = adminCode
                        };
                    }
                    else
                    {
                        UploadServerBatchDetailsResponse response = JsonConvert.DeserializeObject<UploadServerBatchDetailsResponse>(uploadResponse);
                        batchDetails = new BatchDetails()
                        {
                            adminCode = response.data.adminCode,
                            code = response.data.code
                        };
                    }

                    uploadSuccess = true;
                }
                else //web api sent error response 
                {
                    Logger.Log($"Something went wrong while uploading a file. API {URL} is not reachable.");
                    uploadSuccess = false;                    
                }
            }

            fileStream.Dispose();
            fileContent.Dispose();

            return uploadSuccess;
        }

    }
}
