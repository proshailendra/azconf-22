using ePizzaHub.Entities;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Interfaces
{
    public interface IAuthenticationService
    {
        bool CreateUser(User user, string Password, string Role);

        Task<bool> SignOut();

        User AuthenticateUser(string Username, string Password);

        User GetUser(string Username);
    }
}