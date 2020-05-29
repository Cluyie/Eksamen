using AdminPanel.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Client.Services
{
    public interface IReservationService
    {
        Task<Reservation> GetFromId(Guid id);
        Task<Reservation> GetFromResourceById(Guid id);
    }
}
