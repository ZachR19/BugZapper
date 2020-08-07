using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;

namespace BugZapper.Services
{
    public static class AzureKeyVaultService
    {
        public static string GetVaultValue(string Key)
        {
            //Get config data from Azure
            SecretClientOptions options = new SecretClientOptions()
            {
                Retry =
                {
                    Delay= TimeSpan.FromSeconds(1),
                    MaxDelay = TimeSpan.FromSeconds(15),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                }
            };

            var client = new SecretClient(new Uri("https://bugzapperkeyvault.vault.azure.net/"), new DefaultAzureCredential(), options);

            KeyVaultSecret secret = client.GetSecret(Key);

            return secret.Value;
        }

    }
}
