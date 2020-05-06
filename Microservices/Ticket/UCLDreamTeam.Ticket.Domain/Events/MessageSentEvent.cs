using RabbitMQ.Bus.Events;
using MessageEvent = UCLDreamTeam.Ticket.Domain.Models.Message;

namespace UCLDreamTeam.Ticket.Domain.Events
{
    public class MessageSentEvent : Event
    {
        public MessageEvent Message { get; set; }

        public MessageSentEvent(MessageEvent message)
        {
            Message = message;
        }
    }
}
