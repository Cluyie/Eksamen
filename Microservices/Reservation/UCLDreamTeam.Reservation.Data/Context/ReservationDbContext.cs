using Microsoft.EntityFrameworkCore;

namespace UCLDreamTeam.Reservation.Data.Context
{
    public class ReservationDbContext : DbContext
    {
        public ReservationDbContext(DbContextOptions options) : base(options) {}

        public DbSet<Domain.Models.Reservation> Reservations { get; set; }

    }
}
