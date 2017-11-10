using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System;

namespace ShareFiles
{
    public static class GetAvailableFiles
    {
        [FunctionName("GetAvailableFiles")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get")]HttpRequestMessage req, TraceWriter log)
        {
            try
            {
                string conn = await Helper.GetConnectionString();
                CloudStorageAccount acct = CloudStorageAccount.Parse(conn);
                CloudBlobClient client = acct.CreateCloudBlobClient();

                string containerName = Helper.GetContainerForCurrentUser("");
                CloudBlobContainer container = client.GetContainerReference(containerName);

                var blobs = container.ListBlobs(useFlatBlobListing: true);
                List<DataFile> uris = new List<DataFile>();
                string json = "";
                foreach (var blob in blobs)
                {
                    uris.Add(new DataFile()
                    {
                        Uri = blob.Uri.AbsolutePath
                    });

                    json = JsonConvert.SerializeObject(uris, Formatting.Indented);
                }
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
            }
            catch (Exception oEx)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(oEx.ToString(), Encoding.UTF8, "application/text")
                };

            }
        }


        class DataFile
        {
            public string Uri;
        }
    }
}
