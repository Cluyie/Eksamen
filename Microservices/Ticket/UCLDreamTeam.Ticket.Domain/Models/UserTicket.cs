using System;

namespace UCLDreamTeam.Ticket.Domain.Models
{
    public class UserTicket
    {
        public Guid UserId { get; set; }
        public Guid TicketId { get; set; }
        public User User { get; set; }
        public Ticket Ticket { get; set; }
    }
}
