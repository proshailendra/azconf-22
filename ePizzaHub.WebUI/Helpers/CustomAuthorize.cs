using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ePizzaHub.WebUI.Helpers
{
    public class CustomAuthorize : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //authentication
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                //TO DO:  Fetch permission from db

                //authorization
                if (!context.HttpContext.User.IsInRole(Roles))
                {
                    context.Result = new RedirectToActionResult("Unauthorize", "Account", new { area = "" });
                }
            }
            else
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "" });
            }
        }
    }
}