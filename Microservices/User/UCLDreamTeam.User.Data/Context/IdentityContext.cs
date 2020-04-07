using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UCLDreamTeam.User.Domain;

namespace UCLDreamTeam.User.Data.Context
{
    public class IdentityContext : IdentityDbContext<Domain.Models.User, IdentityRole<Guid>, Guid>
    {
        public IdentityContext() : base()
        {
        }

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
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
