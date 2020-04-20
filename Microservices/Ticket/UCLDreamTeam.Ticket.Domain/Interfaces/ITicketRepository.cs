using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UCLDreamTeam.Ticket.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Models.Ticket>> GetAsync();
        Task<Models.Ticket> GetByIdAsync(Guid id);
        Task AddAsync(Models.Ticket reservation);
        Task CancelById(Guid id);
    }
}
