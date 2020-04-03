using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Identity;

namespace Data_Access_Layer.Context
{
  public class IdentityContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
  {
    public IdentityContext() : base()
    {
    }

    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer(Settings.Default.UCLDB);
      }
    }
  }
}

