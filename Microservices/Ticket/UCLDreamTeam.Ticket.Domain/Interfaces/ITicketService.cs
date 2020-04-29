using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces;
using UCLDreamTeam.Ticket.Domain.Events;

namespace UCLDreamTeam.Ticket.Domain.Interfaces
{
    public interface ITicketService
    {
        Task<Domain.Models.Ticket> GetByIdAsync(Guid id);
        Task<IEnumerable<Domain.Models.Ticket>> GetByUserIdAsync(Guid id);
        Task AddAsync(Models.Ticket ticket);
        Task UpdateAsync(Models.Ticket ticket);
        Task AddMessageAsync(Domain.Models.Message message);
        Task ChangeStatusById(Guid id, Status status);
        Task MessageSeen(Guid messageId, bool seen);
    }
}
