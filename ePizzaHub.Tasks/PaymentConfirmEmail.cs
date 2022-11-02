using System;
using System.Net.Mail;
using System.Net;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ePizzaHub.Tasks.Models;

namespace ePizzaHub.ServiceBus
{
    public class PaymentConfirmEmail
    {
        [FunctionName("PaymentConfirmEmail")]
        public static void Run([ServiceBusTrigger("paymentqueue", Connection = "ServiceBusConnection")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

            string smtp = Environment.GetEnvironmentVariable("smtp");
            string email = Environment.GetEnvironmentVariable("email");
            string password = Environment.GetEnvironmentVariable("password");
            int port = Convert.ToInt32(Environment.GetEnvironmentVariable("port"));

            var deserializedMessage = JsonConvert.DeserializeObject<PaymentDetails>(myQueueItem);
            string receipeient = deserializedMessage.Email;
            string receiptId = deserializedMessage.Id;
            string transactionId = deserializedMessage.TransactionId;
            string status = deserializedMessage.Status;
            string currentCurrency = deserializedMessage.Currency;
            decimal total = deserializedMessage.Total;
            decimal tax = deserializedMessage.Tax;
            decimal grandTotal = deserializedMessage.GrandTotal;

            StringBuilder body = new StringBuilder();
            body.AppendLine($"<html xmlns='http://www.w3.org/1999/xhtml'>");
            body.AppendLine($"<head><title></title></head>");
            body.AppendLine($"<table>");
            body.AppendLine($"<tr>");
            body.AppendLine($"<td>");
            body.AppendLine($"<br /><br />");
            body.AppendLine($"<div style='border-top:3px solid'></div>");
            body.AppendLine($"<span style='font-family:Arial;font-size:10pt'>");
            body.AppendLine($"Hello <b>{receipeient} </b><br /> <br />");
            body.AppendLine($"INVOICE: <b>{receiptId}</b><br /><br />");
            body.AppendLine($"Transaction Id: <b>{transactionId}</b> <br/><br />");
            body.AppendLine($"Status: <b>{status}</b> <br/><br />");
            body.AppendLine($"Total: <b>{total}</b> <br/><br />");
            body.AppendLine($"Tax: <b>{tax}</b> <br/><br />");
            body.AppendLine($"Grand Total: <b>{currentCurrency} {grandTotal}</b> <br/><br />");
            body.AppendLine($"<b>Thanks</b> <br/><br />");
            body.AppendLine($"<b>ePizzaHub</b> <br/><br />");
            body.AppendLine($"</span>");
            body.AppendLine($"</td>");
            body.AppendLine($"</tr>");
            body.AppendLine($"</table>");
            body.AppendLine($"</body>");
            body.AppendLine($"</html>");

            MailMessage mailmsg = new MailMessage();
            mailmsg.IsBodyHtml = true; //our body text is html
            mailmsg.From = new MailAddress(email);
            mailmsg.To.Add(receipeient.ToString());
            mailmsg.Subject = "Your Payment Invoice";
            mailmsg.Body = body.ToString();

            try
            {
                var smtpClient = new SmtpClient(smtp)
                {
                    Port = port,
                    Credentials = new NetworkCredential(email, password),
                    EnableSsl = true
                };
                smtpClient.Send(mailmsg);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
            }
            log.LogInformation($"Successfully sent!!");
        }
    }
}
