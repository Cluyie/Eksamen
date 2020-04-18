using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UCLDreamTeam.Resource.Domain.Models;

namespace UCLDreamTeam.Resource.Data.Context
{
    public class ResourceContext : DbContext
    {
        public DbSet<Domain.Models.Resource> Resources { get; set; }
        public DbSet<AvailableTime> AvailableTimes { get; set; }
        public ResourceContext(DbContextOptions options) : base(options)
        {

        }
    }
}
