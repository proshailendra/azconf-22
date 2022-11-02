using System;

namespace ePizzaHub.Entities
{
    public class PaymentDetails
    {
        public string Id { get; set; }
        public string TransactionId { get; set; }
        public decimal Tax { get; set; }
        public string Currency { get; set; }
        public decimal Total { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public Guid CartId { get; set; }
        public int UserId { get; set; }
        public decimal GrandTotal { get; set; }
    }
}