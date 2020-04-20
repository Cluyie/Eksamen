using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Bus.Events;
using Message = UCLDreamTeam.Ticket.Domain.Models.Message;

namespace UCLDreamTeam.Ticket.Domain.Events
{
    public class MessageSentEvent : Event
    {
        public Message Message { get; set; }
        public Guid TicketId { get; set; }

        public MessageSentEvent(Message message, Guid ticketId)
        {
            Message = message;
            TicketId = ticketId;
        }

    }
}
