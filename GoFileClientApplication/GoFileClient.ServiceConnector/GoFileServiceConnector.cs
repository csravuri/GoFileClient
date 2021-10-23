using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GoFileClient.Logger;
using GoFileClient.Models;

namespace GoFileClient.ServiceConnector
{
    public class GoFileServiceConnector : IGoFileServiceConnector
    {
        public GoFileServiceConnector(ILogger logger)
        {
            this.logger = logger;
        }

        #region IGoFileServiceConnector

        bool IGoFileServiceConnector.UploadFileAsync(string fileFullPath)
        {
            var result = false;

            return result;
        }

        #endregion IGoFileServiceConnector

        #region Implementation

        private bool CreateAccount()
        {
            return true;
        }

        private async Task<ServerDetails> GetServerAsync()
        {
            //Action<HttpProgressEventArgs> callback;
            //ProgressMessageHandler progress = new ProgressMessageHandler();
            var result = new ServerDetails();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseURL);

                var response = null as HttpResponseMessage;

                try
                {
                    response = await client.GetAsync("getServer");
                }
                catch (AggregateException ex)
                {
                    logger.Log(ex.Message);
                    if (ex.InnerException != null)
                    {
                        logger.Log(ex.InnerException.Message);
                    }

                    if (ex.InnerExceptions != null)
                    {
                        logger.Log(ex.InnerExceptions.Select(e => e.Message));
                    }

                    throw;
                }
                catch (Exception ex)
                {
                    logger.Log(ex.Message);
                }

                if (response != null && response.IsSuccessStatusCode)
                {
                    await response.Content.ReadAsStringAsync();
                }
            }

            return result;
        }

        private const string apiBaseURL = "https://api.gofile.io/";
        private readonly ILogger logger;

        #endregion Implementation
    }
}