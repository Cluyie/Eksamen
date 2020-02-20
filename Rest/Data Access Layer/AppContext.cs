using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer
{
    public class AppContext : DbContext
    {
        public DbSet<UserData> UserData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("NOT IMPLEMENTET");
        }
    }
}
