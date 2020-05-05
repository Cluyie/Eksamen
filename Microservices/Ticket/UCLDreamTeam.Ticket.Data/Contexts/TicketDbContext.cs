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
        public DbSet<MessageEvent> Messages { get; set; }

        public TicketDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(Settings.Default.UCLDB);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserTicket>()
                .HasKey(ut => new {ut.UserId, ut.TicketId});

            //builder.Entity<UserTicket>()
            //  .HasOne(ut => ut.User)
            //  .WithMany(b => b.UserTickets)
            //  .HasForeignKey(bc => bc.UserId);

            //builder.Entity<UserTicket>()
            //  .HasOne(ut => ut.Ticket)
            //  .WithMany(c => c.UserTickets)
            //  .HasForeignKey(bc => bc.TicketId);
        }
    }
}