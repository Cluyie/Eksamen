using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
