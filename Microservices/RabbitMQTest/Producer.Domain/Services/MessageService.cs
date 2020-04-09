using System.Threading.Tasks;
using Producer.Domain.Commands;
using Producer.Domain.Services.Interfaces;
using RabbitMQ.Bus.Bus.Interfaces;

namespace Producer.Domain.Services
{
    public class MessageService : IMessageService
    {
        private readonly IEventBus _eventBus;

        public MessageService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Send(string message)
        {
            var command = new CreateMessageCommand(message);
            await _eventBus.SendCommand(command);
        }
    }
}