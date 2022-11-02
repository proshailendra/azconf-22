using ePizzaHub.Services.Interfaces;
using System;
using System.Net;
using System.Net.Mail;

namespace ePizzaHub.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private string _mailHost;
        private int _port = 587;
        private string _fromEmail;
        private string _username;
        private string _password;

        public EmailService()
        {
            _mailHost = "";
            _port = 25;
            _fromEmail = "";
            _username = "";
            _password = "";
        }

        public bool SendMail(string to, string subject, string body)
        {
            try
            {
                MailMessage mm = new MailMessage();
                mm.To.Add(to);
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = true;

                bool mailSentStatus = EmailHostConfigurationSendMail(mm);
                if (mailSentStatus == true)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                // throw ex;
            }
            return false;
        }

        private bool EmailHostConfigurationSendMail(MailMessage mm)
        {
            try
            {
                mm.From = new MailAddress(_fromEmail);
                SmtpClient smtp = new SmtpClient();
                smtp.Host = _mailHost;
                smtp.EnableSsl = true;
                smtp.Port = _port;
                NetworkCredential NetworkCred = new NetworkCredential(_username, _password);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Send(mm);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}