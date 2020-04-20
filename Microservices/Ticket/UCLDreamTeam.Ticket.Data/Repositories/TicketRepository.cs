using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UCLDreamTeam.Ticket.Domain.Interfaces;

namespace UCLDreamTeam.Ticket.Data.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        public TicketRepository()
        {
            
        }

        public Task<IEnumerable<Domain.Models.Ticket>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Models.Ticket> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Domain.Models.Ticket reservation)
        {
            throw new NotImplementedException();
        }

        public Task CancelById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
