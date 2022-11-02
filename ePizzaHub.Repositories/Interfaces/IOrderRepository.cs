using ePizzaHub.Entities;
using ePizzaHub.Repositories.Models;
using System.Collections.Generic;

namespace ePizzaHub.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        OrderModel GetOrderDetails(string id);

        IEnumerable<Order> GetUserOrders(int UserId);

        PagingListModel<OrderModel> GetOrderList(int page, int pageSize);
    }
}