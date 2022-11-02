using System;
using System.Text.Json.Serialization;

namespace ePizzaHub.Entities
{
    public class CartItem
    {
        public CartItem()
        {
            // required by EF
        }

        public CartItem(int itemId, int quantity, decimal unitPrice)
        {
            ItemId = itemId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public int Id { get; set; }
        public Guid CartId { get; set; }
        public int ItemId { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public Cart Cart { get; set; }
    }
}