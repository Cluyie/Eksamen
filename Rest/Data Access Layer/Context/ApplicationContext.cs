using System;
using System.Collections.Generic;
using System.Text;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data_Access_Layer.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public ApplicationContext() : base() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Settings.Default.UCLDB);
            }            
        }
    }
}
