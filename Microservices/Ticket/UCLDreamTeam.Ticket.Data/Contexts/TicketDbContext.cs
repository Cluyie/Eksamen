using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using UCLDreamTeam.Ticket.Domain.Models;

namespace UCLDreamTeam.Ticket.Data.Contexts
{
    public class TicketDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Domain.Models.Ticket> Tickets { get; set; }
        public DbSet<UserTicket> UserTickets { get; set; }

        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(Settings.Default.UCLDB);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserTicket>().HasKey(table => new {table.UserId, table.TicketId});
        }
    }
}