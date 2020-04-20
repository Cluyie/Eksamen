using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UCLDreamTeam.Ticket.Domain.Models;

namespace UCLDreamTeam.Ticket.Data.Contexts
{
    public class TicketDbContext : DbContext
    {
        public DbSet<Domain.Models.Ticket> Tickets { get; set; }
        public DbSet<Message> Messages { get; set; }

        public TicketDbContext()
        {
            
        }
    }
}
