using RabbitMQ.Bus.Events;

namespace Producer.Domain.Events
{
    public class MessageCreatedEvent : Event
    {
        public MessageCreatedEvent(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}