using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Bus.Events;

namespace Consumer.Domain.Events
{
    public class MessageCreatedEvent : Event
    {
        public string Message { get; set; }

        public MessageCreatedEvent(string message)
        {
            Message = message;
        }
    }
}