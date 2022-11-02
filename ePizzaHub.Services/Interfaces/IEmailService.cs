namespace ePizzaHub.Services.Interfaces
{
    public interface IEmailService
    {
        bool SendMail(string to, string subject, string body);
    }
}