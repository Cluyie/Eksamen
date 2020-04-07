using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCLDreamTeam.Reservation.Application.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<Domain.Models.Reservation>> GetReservationsAsync();
        Task AddAsync(Domain.Models.Reservation reservation);
    }
}
