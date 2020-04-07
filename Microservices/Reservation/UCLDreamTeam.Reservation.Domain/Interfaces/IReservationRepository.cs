using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCLDreamTeam.Reservation.Domain.Interfaces
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Domain.Models.Reservation>> GetReservationsAsync();
        Task AddAsync(Domain.Models.Reservation reservation);
    }
}