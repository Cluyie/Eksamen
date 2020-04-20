using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCLDreamTeam.Ticket.Domain.Models;

namespace UCLDreamTeam.Ticket.Domain.Events
{
    public class MessageSentEvent
    {
        public Message Message { get; set; }

        public MessageSentEvent(Message message)
        {
            Message = message;
        }

    }
}
