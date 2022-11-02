using System;
using System.Collections.Generic;

namespace ePizzaHub.Repositories.Models
{
    public class OrderModel
    {
        public OrderModel()
        {
            Items = new List<ItemModel>();
        }

        public string Id { get; set; }
        public string PaymentId { get; set; }
        public int UserId { get; set; }
        public string Currency { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal GrandTotal { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<ItemModel> Items { get; set; }
        public string Locality { get; internal set; }
    }
}