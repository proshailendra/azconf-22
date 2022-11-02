using ePizzaHub.Entities;
using Razorpay.Api;

namespace ePizzaHub.Services.Interfaces
{
    public interface IPaymentService
    {
        string CreateOrder(decimal amount, string currency, string receipt);

        string CapturePayment(string paymentId, string orderId);

        Payment GetPaymentDetails(string paymentId);

        bool VerifySignature(string signature, string orderId, string paymentId);

        int SavePaymentDetails(PaymentDetails model);
    }
}