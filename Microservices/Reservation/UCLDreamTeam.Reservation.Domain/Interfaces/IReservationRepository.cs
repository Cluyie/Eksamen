using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UCLDreamTeam.Reservation.Domain.Interfaces
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Models.Reservation>> GetAsync();
        Task<Models.Reservation> GetByIdAsync(Guid id);
        Task AddAsync(Models.Reservation reservation);
        Task UpdateAsync(Models.Reservation reservation);
        Task CancelById(Guid id);
    }
}