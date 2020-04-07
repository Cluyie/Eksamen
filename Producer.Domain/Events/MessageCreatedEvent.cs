using RabbitMQ.Bus.Events;

namespace Producer.Domain.Events
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