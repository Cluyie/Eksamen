using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UCLDreamTeam.UserServiceApi.Domain;
using UCLDreamTeam.UserServiceApi.Domain.Models;

namespace UCLDreamTeam.UserServiceApi.Domain.Context
{
    public class IdentityContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public IdentityContext() : base() { }

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Settings.Default.UCLDB);
            }
        }
    }
}
