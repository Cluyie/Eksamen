using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UCLDreamTeam.Mail.Domain.Models;

namespace UCLDreamTeam.Mail.Data.Context
{
    public class MailDbContext : DbContext
    {
        public MailDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Resource> Resources { get; set; }
    }
}
