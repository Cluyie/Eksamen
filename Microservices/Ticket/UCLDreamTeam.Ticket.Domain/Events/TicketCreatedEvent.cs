using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Bus.Events;

namespace UCLDreamTeam.Ticket.Domain.Events
{
    public class TicketCreatedEvent : Event
    {
        public Models.Ticket Ticket { get; set; }

        public TicketCreatedEvent(Models.Ticket ticket)
        {
            Ticket = ticket;
        }
    }
}
