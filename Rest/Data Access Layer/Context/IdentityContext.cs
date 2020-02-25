using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Data_Access_Layer.Models;

namespace Data_Access_Layer.Context
{
  public class IdentityContext : IdentityDbContext<User>
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

