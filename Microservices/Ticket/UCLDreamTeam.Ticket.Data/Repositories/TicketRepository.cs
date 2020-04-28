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

        public async Task<Domain.Models.Ticket> GetByIdAsync(Guid id)
        {
            return await _ticketDbContext.Tickets.Include(t => t.Messages)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Domain.Models.Ticket>> GetByUserIdAsync(Guid id)
        {
            var user = await _ticketDbContext.Users
                .Include(u => u.UserTickets)
                .FirstOrDefaultAsync(u => u.Id == id);
            var userTickets = user.UserTickets
                .FindAll(ut => ut.UserId == id);
            return userTickets.Select(ut => ut.Ticket);
        }

        public async Task AddAsync(Domain.Models.Ticket ticket)
        {
            _ticketDbContext.Tickets.Add(ticket);
            await _ticketDbContext.SaveChangesAsync();
        }

        public async Task AddMessageAsync(Message message)
        {
            var ticket = await _ticketDbContext.Tickets.FirstOrDefaultAsync(t => t.Id == message.TicketId);
            ticket.Messages.Add(message);
            await _ticketDbContext.SaveChangesAsync();
        }

        public async Task MessageSeen(Guid messageId, bool seen)
        {
            _ticketDbContext.Tickets.FirstOrDefault(t => t.Messages.Any(m => m.Id == messageId)).Messages
                .Find(m => m.Id == messageId).Seen = seen;
            await _ticketDbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(Domain.Models.Ticket ticket)
        {
            _ticketDbContext.Tickets.Add(ticket);
            await _ticketDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Models.Ticket ticket)
        {
            _ticketDbContext.Tickets.Update(ticket);
            await _ticketDbContext.SaveChangesAsync();
        }


        public async Task ChangeStatusById(Guid id, Status status)
        {
            var ticket = await _ticketDbContext.Tickets.FirstOrDefaultAsync(t => t.Id == id);
            ticket.Status = status;
            await _ticketDbContext.SaveChangesAsync();
        }
    }
}