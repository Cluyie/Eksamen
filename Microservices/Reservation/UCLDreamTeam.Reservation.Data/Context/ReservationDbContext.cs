using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UCLDreamTeam.Reservation.Data.Context
{
    public class ReservationDbContext : DbContext
    {
        public ReservationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Domain.Models.Reservation> Reservations { get; set; }
    }
}
