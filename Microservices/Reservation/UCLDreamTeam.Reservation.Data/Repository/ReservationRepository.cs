﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UCLDreamTeam.Reservation.Data.Context;
using UCLDreamTeam.Reservation.Domain.Interfaces;
using Z.EntityFramework.Plus;

namespace UCLDreamTeam.Reservation.Data.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ReservationDbContext _dbContext;

        public ReservationRepository(ReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Domain.Models.Reservation>> GetAsync()
        {
            return await _dbContext.Reservations.Include(r => r.Timeslot).ToListAsync();
        }

        public async Task<Domain.Models.Reservation> GetByIdAsync(Guid id)
        {
            return await _dbContext.Reservations.Include(r => r.Timeslot).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Domain.Models.Reservation>> GetByUserIdAsync(Guid id)
        {
            return await _dbContext.Reservations
                .Include(r => r.Timeslot)
                .Where(r => r.UserId == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Domain.Models.Reservation>> GetByResourceId(Guid resourceId)
        {
            return await _dbContext.Reservations
                .Include(r => r.Timeslot)
                .Where(r => r.ResourceId == resourceId)
                .ToListAsync();
        }

        public async Task AddAsync(Domain.Models.Reservation reservation)
        {
            await _dbContext.Reservations.AddAsync(reservation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Models.Reservation reservation)
        {
            _dbContext.Reservations.Update(reservation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CancelById(Guid id)
        {
            var reservation = await _dbContext.Reservations
              .FirstOrDefaultAsync(r => r.Id == id);
            _dbContext.Reservations.Remove(reservation);
            await _dbContext.SaveChangesAsync();
        }
    }
}