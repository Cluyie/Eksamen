using System;
using System.Collections.Generic;
using UCLDreamTeam.SharedInterfaces;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Ticket.Domain.Models
{
    public class Ticket : ITicket<MessageEvent>
    {
        public Guid Id { get; set; }
        public bool? Active { get; set; }
        public Status Status { get; set; }

        public List<MessageEvent> Messages { get; set; }

        public List<UserTicket> UserTickets { get; set; }
        public Guid? ReservationId { get; set; }
    }
}