using ePizzaHub.Entities;
using ePizzaHub.Repositories.Models;
using System.Collections.Generic;

namespace ePizzaHub.Services.Interfaces
{
    public interface IOrderService
    {
        OrderModel GetOrderDetails(string OrderId);

        IEnumerable<Order> GetUserOrders(int UserId);

        int PlaceOrder(int userId, string orderId, string paymentId, CartModel cart, Address address);

        PagingListModel<OrderModel> GetOrderList(int page = 1, int pageSize = 10);
    }
}