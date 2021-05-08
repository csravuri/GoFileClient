using GoFileHelper.Common;
using GoFileHelper.Entites;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GoFileServiceConnect
{
    public class GoFileConnector
    {
        private readonly string ApiServerURL = "https://apiv2.gofile.io";
        private readonly string ApiURL = "https://api.gofile.io";

        /// <summary>
        /// Returns false if server details not found
        /// </summary>
        /// <param name="serverAddress"></param>
        /// <returns></returns>
        private bool GetServers(out string serverAddress)
        {            
            serverAddress = null;
                        
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiServerURL);
                //HTTP GET
                var responseTask = client.GetAsync("getServer");

                try
                {
                    responseTask.Wait();
                }
                catch(AggregateException ex)
                {
                    Logger.Log(ex.Message);
                    if (ex.InnerExceptions != null && ex.InnerExceptions.Count > 0)
                    {
                        foreach(var innerEx in ex.InnerExceptions)
                        {
                            Logger.Log(innerEx.Message);
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message);
                    return false;
                }                

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
                    Logger.Log($"Something went wrong while getting active server. API {ApiServerURL} is not reachable.");
                    return false;
                }
            }
        }

        /// <summary>
        /// Return false if file upload not success
        /// </summary>
        /// <param name="fileFullPath">If file doesnot exist nothing will happen</param>
        /// <param name="adminCode">If admin code is not provided, will create new batch</param>
        public bool UploadFile(out BatchDetails batchDetails, string fileFullPath, string adminCode = null, Action<HttpProgressEventArgs> callback = null)
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

            FileStream fileStream = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read, FileShare.None);
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

            ProgressMessageHandler progress = new ProgressMessageHandler();

            if (callback != null)
            {
                progress.HttpSendProgress += new EventHandler<HttpProgressEventArgs>((e, args) => callback(args));
            }

            using (var client = HttpClientFactory.Create(progress))
            {
                client.Timeout = new TimeSpan(23, 0, 0);
                client.BaseAddress = new Uri(URL);
                //HTTP POST
                var responseTask = client.PostAsync("uploadFile", formDataContent);
                try
                {
                    responseTask.Wait(-1);
                }
                catch (AggregateException ex)
                {
                    Logger.Log(ex.Message);
                    if (ex.InnerExceptions != null && ex.InnerExceptions.Count > 0)
                    {
                        foreach (var innerEx in ex.InnerExceptions)
                        {
                            Logger.Log(innerEx.Message);
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message);
                    return false;
                }
                finally
                {
                    fileStream.Close(); 
                    fileStream.Dispose();
                    fileContent.Dispose();
                }

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

            return uploadSuccess;
        }

        public bool GetUpload(string code, string adminCode = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiURL);

                client.DefaultRequestHeaders.Add("c", code);
                if (!string.IsNullOrWhiteSpace(adminCode))
                {
                    client.DefaultRequestHeaders.Add("ac", adminCode);
                }
                //HTTP GET
                var responseTask = client.GetAsync("getUpload?c=oNNorW&ac=L8Tzat3m4PYteCIjY9Lr");

                try
                {
                    responseTask.Wait();
                }
                //catch (AggregateException ex)
                //{
                //    Logger.Log(ex.Message);
                //    if (ex.InnerExceptions != null && ex.InnerExceptions.Count > 0)
                //    {
                //        foreach (var innerEx in ex.InnerExceptions)
                //        {
                //            Logger.Log(innerEx.Message);
                //        }
                //    }
                //    return false;
                //}
                catch (Exception ex)
                {
                    Logger.Log(ex.Message);
                    return false;
                }

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var serverDetailResponse = readTask.Result;
                    Logger.Log(serverDetailResponse);

                    //serverAddress = JsonConvert.DeserializeObject<ServerDetailResponse>(serverDetailResponse).data.server;

                    return true;
                }
                else //web api sent error response 
                {
                    Logger.Log($"Something went wrong while getting active server. API {ApiServerURL} is not reachable.");
                    return false;
                }
            }


        }
    }
}
