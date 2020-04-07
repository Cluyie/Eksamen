using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Bus.Bus.Interfaces;
using TestProducer.Commands;
using TestProducer.Services.Interfaces;

namespace TestProducer.Services
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