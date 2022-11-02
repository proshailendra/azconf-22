using ePizzaHub.Entities;
using ePizzaHub.Repositories.Interfaces;
using ePizzaHub.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace ePizzaHub.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private AppDbContext dbContext
        {
            get
            {
                return _dbContext as AppDbContext;
            }
        }

        public OrderRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public OrderModel GetOrderDetails(string orderId)
        {
            var model = (from order in dbContext.Orders
                         where order.Id == orderId
                         select new OrderModel
                         {
                             Id = order.Id,
                             UserId = order.UserId,
                             CreatedDate = order.CreatedDate,
                             Items = (from orderItem in dbContext.OrderItems
                                      join item in dbContext.Items
                                      on orderItem.ItemId equals item.Id
                                      where orderItem.OrderId == orderId
                                      select new ItemModel
                                      {
                                          Id = orderItem.Id,
                                          Name = item.Name,
                                          Description = item.Description,
                                          ImageUrl = item.ImageUrl,
                                          Quantity = orderItem.Quantity,
                                          ItemId = item.Id,
                                          UnitPrice = orderItem.UnitPrice
                                      }).ToList()
                         }).FirstOrDefault();
            return model;
        }

        public IEnumerable<Order> GetUserOrders(int UserId)
        {
            return dbContext.Orders
               .Include(o => o.OrderItems)
               .Where(x => x.UserId == UserId).ToList();
        }

        public PagingListModel<OrderModel> GetOrderList(int page, int pageSize)
        {
            var pagingModel = new PagingListModel<OrderModel>();
            var data = (from order in dbContext.Orders
                        join payment in dbContext.PaymentDetails
                        on order.PaymentId equals payment.Id
                        select new OrderModel
                        {
                            Id = order.Id,
                            UserId = order.UserId,
                            PaymentId = payment.Id,
                            CreatedDate = order.CreatedDate,
                            GrandTotal = payment.GrandTotal,
                            Locality = order.Locality
                        });

            int itemCounts = data.Count();
            var orders = data.Skip((page - 1) * pageSize).Take(pageSize);

            var pagedListData = new StaticPagedList<OrderModel>(orders, page, pageSize, itemCounts);

            pagingModel.Data = pagedListData;
            pagingModel.Page = page;
            pagingModel.PageSize = pageSize;
            pagingModel.TotalRows = itemCounts;
            return pagingModel;
        }
    }
}