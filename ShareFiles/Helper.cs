using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.KeyVault;

using System.Threading.Tasks;

namespace ShareFiles
{
    public static class Helper
    {
        public static string GetContainerForCurrentUser(string user)
        {
            return "customer1";
        }

        public static async Task<string> GetConnectionString()
        {
            string uriStr = System.Environment.GetEnvironmentVariable("ConnectionSecretUri");
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            
            var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            Microsoft.Azure.KeyVault.Models.SecretBundle secret = await kv.GetSecretAsync(uriStr);
            return secret.Value;
        }
    }
}
