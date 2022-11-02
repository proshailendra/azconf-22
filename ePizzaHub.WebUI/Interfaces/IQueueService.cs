using System.Threading.Tasks;

namespace ePizzaHub.WebUI.Interfaces
{
    public interface IQueueService
    {
        Task SendMessageAsync<T>(T serviceBusMessage, string queueName);
    }
}