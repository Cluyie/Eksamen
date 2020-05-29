using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Ticket.Domain.Models
{
    public class UserTicket : IUserTicket
    {
        public Guid UserId { get; set; }

        //[ForeignKey("UserId")]
        //public User User { get; set; }
        public Guid TicketId { get; set; }
        //[ForeignKey("TicketId")]
        //public Ticket Ticket { get; set; }
    }
}