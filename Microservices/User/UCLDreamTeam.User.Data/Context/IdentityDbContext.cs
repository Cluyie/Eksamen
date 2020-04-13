using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UCLDreamTeam.User.Domain;

namespace UCLDreamTeam.User.Data.Context
{
    public class IdentityDbContext : IdentityDbContext<Domain.Models.User, IdentityRole<Guid>, Guid>
    {
        public IdentityDbContext() : base()
        {
        }

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
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
