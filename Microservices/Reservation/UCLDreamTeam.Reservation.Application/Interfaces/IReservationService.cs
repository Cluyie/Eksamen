using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCLDreamTeam.Reservation.Application.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<Domain.Models.Reservation>> GetAsync();
        Task<Domain.Models.Reservation> GetByIdAsync(Guid id);
        Task AddAsync(Domain.Models.Reservation reservation);
        Task CancelById(Guid id);
    }
}
