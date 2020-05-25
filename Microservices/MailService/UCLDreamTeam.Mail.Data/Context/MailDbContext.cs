using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UCLDreamTeam.Mail.Domain.Models;

namespace UCLDreamTeam.Mail.Data.Context
{
    public class MailDbContext : DbContext
    {
        public MailDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "Admin",
                Email = "DreamTeamUCL@gmail.com",
                FirstName = "Admin",
                LastName = "Adminsen",
            };

            var resource = new Resource
            {
                Id = Guid.NewGuid(),
                Name = "Hegnet's Sure Sokker",
                Description = "Er dine sokker sure? Så udfører Hegnet god service på en omgang rengøring i dine sokker!",
                TimeSlots = new List<AvailableTime>
                {
                    new AvailableTime
                    {
                        From = new DateTime(2020, 06, 11, 08, 00, 00),
                        To = new DateTime(2020, 06, 12, 16, 00, 00),
                        Recurring = true
                    }
                }
            };


            modelBuilder.Entity<User>().HasData(user);

            modelBuilder.Entity<Resource>().HasData(resource);

            base.OnModelCreating(modelBuilder);
        }
    }
}
