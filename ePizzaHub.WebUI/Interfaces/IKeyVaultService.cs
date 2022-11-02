using System.Threading.Tasks;

namespace ePizzaHub.WebUI.Interfaces
{
    public interface IKeyVaultService
    {
        Task<string> GetSecret(string secretName);

        Task<string> SetSecret(string secretName, string secretValue);

        Task<string> DeleteSecret(string secretName);
    }
}