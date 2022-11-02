using System;
using System.Collections.Generic;

namespace ePizzaHub.Repositories.Models
{
    public class CartModel
    {
        public CartModel()
        {
            Items = new List<ItemModel>();
        }

        public Guid Id { get; set; }
        public int UserId { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal GrandTotal { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<ItemModel> Items { get; set; }
    }
}