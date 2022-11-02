using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using ePizzaHub.WebUI.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ePizzaHub.WebUI.Services
{
    public class KeyVaultService : IKeyVaultService
    {
        private readonly SecretClient _secretClient;
        public KeyVaultService(SecretClient secretClient)
        {
            _secretClient = secretClient;
        }
        public async Task<string> GetSecret(string secretName)
        {
            var secret = await _secretClient.GetSecretAsync(secretName);

            return secret.Value.Value;
        }

        public async Task<string> SetSecret(string secretName, string secretValue)
        {
            var secret = await _secretClient.SetSecretAsync(secretName, secretValue);
            return secret.Value.Value;
        }

        public async Task<string> DeleteSecret(string secretName)
        {
            var operation = await _secretClient.StartDeleteSecretAsync(secretName);
            var secret = await operation.WaitForCompletionAsync();
            _secretClient.PurgeDeletedSecret(operation.Value.Name);
            return secret.Value.Value;
        }
    }
}