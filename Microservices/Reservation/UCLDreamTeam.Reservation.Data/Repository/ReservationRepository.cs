using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Reservation.Data.Context;
using UCLDreamTeam.Reservation.Domain.Interfaces;

namespace UCLDreamTeam.Reservation.Data.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ReservationDbContext _dbContext;

        public ReservationRepository(ReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Domain.Models.Reservation>> GetReservationsAsync()
        {
            return await _dbContext.Reservations.ToListAsync();
        }

        public async Task AddAsync(Domain.Models.Reservation reservation)
        {
            await _dbContext.AddAsync(reservation);
        }
    }
}