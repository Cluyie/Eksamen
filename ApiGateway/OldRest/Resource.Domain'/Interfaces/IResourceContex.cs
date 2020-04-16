using Microsoft.EntityFrameworkCore;
using Resource.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resource.Domain.Interfaces
{
    public interface IResourceContex
    {
        public DbSet<Resource<AvaiableTime>> Resources { get; set; }
    }
}
