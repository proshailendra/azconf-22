using System;
using System.Collections.Generic;
using System.Text;

namespace ePizzaHub.Tasks.Models
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
        public decimal GrandTotal { get; set; }
    }
}
