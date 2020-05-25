using RabbitMQ.Bus.Events;
using Message = SignalR_Microservice.Models.Message;

namespace SignalR_Microservice.Events
{
    public class MessageSentEvent : Event
    {
        public Message Message { get; set; }

        public MessageSentEvent(Message message)
        {
            Message = message;
        }
    }
}