
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Resource.Domain.Models;
using Resource.Domain.Interfaces;

namespace ResourceMicrosDtabase
{
    public class ReasourceContext:DbContext, IResourceContex, IAvaiableTimeContex
    {
        public DbSet<Resource<AvaiableTime>> Resources { get; set; }
        public DbSet<AvaiableTime> AvaiableTimes { get; set; }
        public ReasourceContext(DbContextOptions options) : base(options)
        {

        }
    }
}
