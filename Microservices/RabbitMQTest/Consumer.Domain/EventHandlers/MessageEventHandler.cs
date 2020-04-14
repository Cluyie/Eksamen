using System.Threading.Tasks;
using Consumer.Domain.Events;
using Microsoft.Extensions.Logging;
using RabbitMQ.Bus.Bus.Interfaces;

namespace Consumer.Domain.EventHandlers
{
    public class MessageEventHandler : IEventHandler<MessageCreatedEvent>
    {
        private readonly ILogger<MessageEventHandler> _logger;

        public MessageEventHandler(ILogger<MessageEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(MessageCreatedEvent @event)
        {
            _logger.LogInformation("MessageEventHandler Called");
            return Task.CompletedTask;
        }
    }
}