using ePizzaHub.Entities;
using ePizzaHub.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        protected SignInManager<User> _signManager;
        protected UserManager<User> _userManager;
        protected RoleManager<Role> _roleManager;

        public AuthenticationService(SignInManager<User> signManager, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signManager = signManager;
        }

        public User AuthenticateUser(string Username, string Password)
        {
            //lockoutOnFailure default value:5
            var result = _signManager.PasswordSignInAsync(Username, Password, false, lockoutOnFailure: false).Result;
            if (result.Succeeded)
            {
                var user = _userManager.FindByNameAsync(Username).Result;
                var roles = _userManager.GetRolesAsync(user).Result;
                user.Roles = roles.ToArray();

                return user;
            }
            return null;
        }

        public User GetUser(string Username)
        {
            return _userManager.FindByNameAsync(Username).Result;
        }

        public bool CreateUser(User user, string Password, string Role)
        {
            var result = _userManager.CreateAsync(user, Password).Result;
            if (result.Succeeded)
            {
                var res = _userManager.AddToRoleAsync(user, Role).Result;
                if (res.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> SignOut()
        {
            try
            {
                await _signManager.SignOutAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}