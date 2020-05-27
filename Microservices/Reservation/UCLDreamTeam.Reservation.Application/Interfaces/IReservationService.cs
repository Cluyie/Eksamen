using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UCLDreamTeam.Reservation.Application.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<Domain.Models.Reservation>> GetAsync();
        Task<Domain.Models.Reservation> GetByIdAsync(Guid id);
        Task<IEnumerable<Domain.Models.Reservation>> GetByUserIdAsync(Guid id);
        Task<IEnumerable<Domain.Models.Reservation>> GetByResourceId(Guid resourceId);
        Task AddAsync(Domain.Models.Reservation reservation);
        Task UpdateAsync(Domain.Models.Reservation reservation);
        Task CancelById(Guid id);
    }
}