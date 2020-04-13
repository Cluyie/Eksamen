using Microsoft.EntityFrameworkCore;
using ResourceMicrosDtabase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceMicrosDtabase
{
    public class ReasourceContext:DbContext
    {
        DbSet<Resource> Resources { get; set; }
        DbSet<AvaiableTime> AvaiableTimes { get; set; }
    }
}
