using RabbitMQ.Bus.Events;
using Message = UCLDreamTeam.Ticket.Domain.Models.Message;

namespace UCLDreamTeam.Ticket.Domain.Events
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
