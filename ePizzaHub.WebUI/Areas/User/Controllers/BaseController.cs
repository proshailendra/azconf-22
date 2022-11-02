using ePizzaHub.WebUI.Helpers;
using ePizzaHub.WebUI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [CustomAuthorize(Roles = "User")]
    public class BaseController : Controller
    {
        public IUserAccessor _userAccessor { get; set; }

        public Entities.User CurrentUser
        {
            get
            {
                if (User != null)
                    return _userAccessor.GetUser();
                else
                    return null;
            }
        }

        public BaseController(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }
    }
}