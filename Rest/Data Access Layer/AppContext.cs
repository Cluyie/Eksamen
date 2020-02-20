using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer
{
    public class AppContext : DbContext
    {
        public AppContext() : base() { }

        public AppContext(DbContextOptions<AppContext> options) : base(options) { }

        public DbSet<UserData> UserData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Settings.Default.UCLDB);
            }
            
        }
    }
}
