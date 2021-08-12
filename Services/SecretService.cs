using WebApi.Helpers;
using Google.Cloud.SecretManager.V1;


namespace WebApi.Services
{
    public interface ISecretService
    {
        string GetSecret(string key);

    }

    public class SecretService : ISecretService
    {
        private SecretManagerServiceClient client;

        public SecretService()
        {
            this.client = SecretManagerServiceClient.Create();
        }

        public string GetSecret(string key)
        {
            AccessSecretVersionResponse result = client.AccessSecretVersion($"projects/807819608558/secrets/{key}/versions/latest");
            return result.Payload.Data.ToStringUtf8();
        }

    }
}