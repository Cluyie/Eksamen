using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UCLDreamTeam.User.Domain;

namespace UCLDreamTeam.User.Data.Context
{
    public class UserDbContext : DbContext
    {
        public DbSet<Domain.Models.User> Users { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Settings.Default.UCLDB);
            }
        }
    }
}
