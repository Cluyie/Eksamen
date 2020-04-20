using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UCLDreamTeam.SharedInterfaces;
using UCLDreamTeam.Ticket.Data.Contexts;
using UCLDreamTeam.Ticket.Domain.Interfaces;
using UCLDreamTeam.Ticket.Domain.Models;
using Z.EntityFramework.Plus;

namespace UCLDreamTeam.Ticket.Data.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketDbContext _ticketDbContext;

        public TicketRepository(TicketDbContext ticketDbContext)
        {
            _ticketDbContext = ticketDbContext;
        }

        public async Task<IEnumerable<Domain.Models.Ticket>> GetAsync()
        {
            return await _ticketDbContext.Tickets.Include(t => t.Messages)
                .ToListAsync();
        }

        public async Task<Domain.Models.Ticket> GetByIdAsync(Guid id)
        {
            return await _ticketDbContext.Tickets.Include(t => t.Messages)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Domain.Models.Ticket ticket)
        {
            await _ticketDbContext.Tickets.AddAsync(ticket);
            await _ticketDbContext.SaveChangesAsync();
        }

        public async Task MessageSeen(Message message)
        {
            var dbMessage = await _ticketDbContext.Messages.FirstOrDefaultAsync(m => m.Id == message.Id);
            dbMessage.Seen = true;
            await _ticketDbContext.SaveChangesAsync();
        }

        public async Task ChangeStatusById(Guid id, Status status)
        {
            await _ticketDbContext.Tickets.Where(r => r.Id == id).DeleteAsync();
            await _ticketDbContext.SaveChangesAsync();
        }
    }
}
