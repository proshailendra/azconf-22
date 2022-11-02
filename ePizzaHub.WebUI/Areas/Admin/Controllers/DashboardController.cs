using ePizzaHub.Repositories.Models;
using ePizzaHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.WebUI.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        private IOrderService _orderService;

        public DashboardController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index(int page = 1, int pageSize = 2)
        {
            var orders = _orderService.GetOrderList(page, pageSize);
            return View(orders);
        }

        [Route("~/Admin/Dashboard/Details/{OrderId}")]
        public IActionResult Details(string OrderId)
        {
            OrderModel Order = _orderService.GetOrderDetails(OrderId);
            return View(Order);
        }
    }
}