using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consumer.Domain.Events;
using Microsoft.Extensions.Logging;
using RabbitMQ.Bus.Bus.Interfaces;

namespace Consumer.Domain.EventHandlers
{
    public class MessageEventHandler : IEventHandler<MessageCreatedEvent>
    {
        public Task Handle(MessageCreatedEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}