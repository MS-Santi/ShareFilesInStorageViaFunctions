Steps to setup project
 
Create/Identify the storage account where customer files will be stored for their download.
Gather the storage account's connection string:
 
Create a KeyVault and add a secret to it: https://docs.microsoft.com/en-us/azure/key-vault/key-vault-get-started#vault
The secret has an associated URI. Take note of the URI for the secret
 
Create a Function Application
Configure Functions to use MSI https://docs.microsoft.com/en-us/azure/app-service/app-service-managed-service-identity
Configure Application settings. Add a setting called DataStorageConnection, and use the URI to the secret you saved in step 3.a: https://docs.microsoft.com/en-us/azure/azure-functions/functions-how-to-use-azure-function-app-settings#settings
 
