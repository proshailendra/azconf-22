using ePizzaHub.WebUI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.WebUI.Areas.User.Controllers
{
    public class DashboardController : BaseController
    {
        public DashboardController(IUserAccessor userAccessor) : base(userAccessor)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}