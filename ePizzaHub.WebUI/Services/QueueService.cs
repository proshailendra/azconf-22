using Azure.Messaging.ServiceBus;
using ePizzaHub.WebUI.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Threading.Tasks;

namespace ePizzaHub.WebUI.Services
{
    public class QueueService : IQueueService
    {
        ServiceBusClient _serviceBusClient;
        IConfiguration _configuration;
        ServiceBusSender _sender;
        IKeyVaultService _keyVaultService;
        public QueueService(IConfiguration configuration, IKeyVaultService keyVaultService)
        {
            _configuration = configuration;
            _keyVaultService = keyVaultService;
            //_serviceBusClient = new ServiceBusClient(_configuration["ConnectionStrings:ServiceBus"]);

            var ServiceBus = _keyVaultService.GetSecret("ServiceBus").Result;
            _serviceBusClient = new ServiceBusClient(ServiceBus);
        }

        public async Task SendMessageAsync<T>(T serviceBusMessage, string queueName)
        {
            _sender = _serviceBusClient.CreateSender(queueName);
            string messageBody = JsonSerializer.Serialize(serviceBusMessage);
            var message = new ServiceBusMessage(messageBody);

            await _sender.SendMessageAsync(message);
        }
    }
}