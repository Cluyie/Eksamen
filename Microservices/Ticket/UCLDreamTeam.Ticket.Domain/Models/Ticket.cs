using System;
using System.Collections.Generic;
using UCLDreamTeam.SharedInterfaces;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Ticket.Domain.Models
{
    public class Ticket : ITicket<Message>
    {
        public Guid Id { get; set; }
        public bool? Active { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }

        public List<Message> Messages { get; set; }

        public List<UserTicket> UserTickets { get; set; }
        public Guid? ReservationId { get; set; }
    }
}