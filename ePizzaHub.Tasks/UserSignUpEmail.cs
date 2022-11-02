using ePizzaHub.ServiceBus.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ServiceBusEmail
{
    public static class UserSignUpEmail
    {
        [FunctionName("UserSignUpEmail")]
        public static void Run([ServiceBusTrigger("userqueue", Connection = "ServiceBusConnection")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            
            string smtp = Environment.GetEnvironmentVariable("smtp");
            string email = Environment.GetEnvironmentVariable("email");
            string password = Environment.GetEnvironmentVariable("password");
            int port = Convert.ToInt32(Environment.GetEnvironmentVariable("port"));

            var deserializedMessage = JsonConvert.DeserializeObject<User>(myQueueItem);
            string receipeient = deserializedMessage.Email;
            string user = deserializedMessage.UserName;

            StringBuilder body = new StringBuilder();
            body.AppendLine($"Dear {user} a warm welcome from us at ePizzaHub!");
            body.AppendLine();
            body.AppendLine($"Please login at www.epizzahub.com");
            body.AppendLine();
            body.AppendLine();
            body.AppendLine($"Regards,");
            body.AppendLine();
            body.AppendLine($"Team ePizzaHub");

            MailMessage mailmsg = new MailMessage();
            mailmsg.IsBodyHtml = true; //our body text is html
            mailmsg.From = new MailAddress(email);
            mailmsg.To.Add(receipeient.ToString());
            mailmsg.Subject = "Welcome to ePizzaHub";
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