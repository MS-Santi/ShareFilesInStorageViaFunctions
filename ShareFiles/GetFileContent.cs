using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ShareFiles
{
    public static class GetFileContent
    {
        [FunctionName("GetFileContent")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get")]HttpRequestMessage req, TraceWriter log)
        {
            try
            {
                string name = req.GetQueryNameValuePairs()
                    .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                    .Value;

                string conn = await Helper.GetConnectionString();
                CloudStorageAccount acct = CloudStorageAccount.Parse(conn);
                CloudBlobClient client = acct.CreateCloudBlobClient();

                Uri uri = new Uri(client.BaseUri, name);

                ICloudBlob blobRef = client.GetBlobReferenceFromServer(uri);
                string fileContent = "";

                MemoryStream memStream = new MemoryStream();
                blobRef.DownloadToStream(memStream);

                StreamReader reader = new StreamReader(memStream);
                memStream.Position = 0;
                fileContent = reader.ReadToEnd();

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(fileContent, Encoding.UTF8, "application/json")
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
    }
}
