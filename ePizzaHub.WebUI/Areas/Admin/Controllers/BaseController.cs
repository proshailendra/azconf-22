using ePizzaHub.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CustomAuthorize(Roles = "Admin")]
    public class BaseController : Controller
    {
    }
}