using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace UCLDreamTeam.Ticket.Data.Contexts
{
    public class TicketDbContext : DbContext
    {
        public DbSet<Domain.Models.Ticket> Tickets { get; set; }

        public TicketDbContext()
        {
            
        }
    }
}
