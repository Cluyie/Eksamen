using Microsoft.EntityFrameworkCore;
using ResourceMicrosDtabase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceMicrosDtabase
{
    public class ReasourceContext:DbContext
    {
        public DbSet<Resource<AvaiableTime>> Resources { get; set; }
        public DbSet<AvaiableTime> AvaiableTimes { get; set; }
    }
}
