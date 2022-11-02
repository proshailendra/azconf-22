using ePizzaHub.Entities;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.WebUI.Helpers;
using ePizzaHub.WebUI.Interfaces;
using ePizzaHub.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ePizzaHub.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationService _authService;
        private IQueueService _queueService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger, IAuthenticationService authService, IQueueService queueService)
        {
            _authService = authService;
            _logger = logger;
            _queueService = queueService;
        }

       // [CustomExceptionFilter]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model, string returnUrl)
        {
            var user = _authService.AuthenticateUser(model.Email, model.Password);
            if (user != null)
            {
                if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                if (user.Roles.Contains("Admin"))
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                else if (user.Roles.Contains("User"))
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "User" });
                }
            }
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(UserModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
                bool result = _authService.CreateUser(user, model.Password, "User");
                if (result)
                {
                    UserModelEmailTemplate usr = new UserModelEmailTemplate
                    {
                        UserName = model.Name,
                        Email = model.Email
                    };

                    //Adding to queue the user details to send email
                    _queueService.SendMessageAsync(usr, "userqueue");
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        public IActionResult UnAuthorize()
        {
            return View();
        }

        public IActionResult LogOutComplete()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _authService.SignOut();
            return RedirectToAction("LogOutComplete");
        }
    }
}