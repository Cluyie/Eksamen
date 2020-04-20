using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces;

namespace UCLDreamTeam.Ticket.Application.Interfaces
{
    public interface ITicketService
    {
        Task<Domain.Models.Ticket> GetByIdAsync(Guid id);
        Task<IEnumerable<Domain.Models.Ticket>> GetByUserIdAsync(Guid id);
        Task AddAsync(Domain.Models.Message message);
        Task ChangeStatusById(Guid id, Status status);
        Task MessageSeen(Guid messageId, bool seen);
    }
}
