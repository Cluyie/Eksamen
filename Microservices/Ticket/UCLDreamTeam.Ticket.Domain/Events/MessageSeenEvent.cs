using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Bus.Events;

namespace UCLDreamTeam.Ticket.Domain.Events
{
    public class MessageSeenEvent : Event
    {
        private MessageSeenEvent(Guid messageId, bool seen)
        {
            MessageId = messageId;
            Seen = seen;
        }

        public Guid MessageId { get; set; }
        public bool Seen { get; set; }
    }
}
