using ePizzaHub.Entities;
using ePizzaHub.WebUI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ePizzaHub.WebUI.Helpers
{
    public class UserAccessor : IUserAccessor
    {
        private readonly UserManager<User> _userManager;
        private IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public User GetUser()
        {
            if (_httpContextAccessor.HttpContext.User != null)
                return _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;
            else
                return null;
        }
    }
}