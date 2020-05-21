using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCLDreamTeam.Auth.Api.Models;

namespace UCLDreamTeam.Auth.Api.Infrastructure
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {
        }

        public DbSet<AuthUser> AuthUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.AuthUserId, ur.RoleId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(Settings.Default.UCLDB);
        }
    }
}
