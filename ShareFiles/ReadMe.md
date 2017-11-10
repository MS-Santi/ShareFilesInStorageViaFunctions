Steps to setup project
 
1. Create/Identify the storage account where customer files will be stored for their download.
1.a Gather the storage account's connection string.
 
2. Create a KeyVault and add a secret to it: https://docs.microsoft.com/en-us/azure/key-vault/key-vault-get-started#vault
2.a The secret has an associated URI. Take note of the URI for the secret
 
3. Create a Function Application
3.a Configure Functions to use MSI https://docs.microsoft.com/en-us/azure/app-service/app-service-managed-service-identity
3.b Configure Application settings. Add a setting called DataStorageConnection, and use the URI to the secret you saved in step 2.a: https://docs.microsoft.com/en-us/azure/azure-functions/functions-how-to-use-azure-function-app-settings#settings
 
Open Solution in Visual Studio (Azure Functions Tools required). Deploy to the Azure Function Created in step 3
