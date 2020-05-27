using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces;
using UCLDreamTeam.Ticket.Domain.Models;

namespace UCLDreamTeam.Ticket.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task<Models.Ticket> GetByIdAsync(Guid id);
        Task<IEnumerable<Models.Ticket>> GetByUserIdAsync(Guid id);
        Task AddAsync(Models.Ticket ticket);
        Task AddMessageAsync(Message message);
        Task ChangeStatusById(Guid id, Status status);
        Task MessageSeen(Guid messageId, bool seen);
        Task CreateAsync(Models.Ticket ticket);
        Task UpdateAsync(Models.Ticket ticket);
        Task AddUserAsync(User user);
        Task DeleteUserAsync(User user);
    }
}