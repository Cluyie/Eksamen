using Microsoft.EntityFrameworkCore;
using ResourceMicrosDtabase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceMicrosDtabase.Context
{
    class ResourceContext: DbContext
    {
        public DbSet<Resource> Resources { get; set; }
        public DbSet<AvaiableTime> avaiableTimes { get; set; }

        public ResourceContext() : base() { }
        public ResourceContext(DbContextOptions<ResourceContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Settings.Default.UCLDB);
            }
        }
    }
}
