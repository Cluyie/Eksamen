using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces;
using UCLDreamTeam.Ticket.Application.Interfaces;
using UCLDreamTeam.Ticket.Domain.Interfaces;

namespace UCLDreamTeam.Ticket.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<Domain.Models.Ticket>> GetAsync()
        {
            return await _ticketRepository.GetAsync();
        }

        public async Task<Domain.Models.Ticket> GetByIdAsync(Guid id)
        {
            return await _ticketRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Domain.Models.Message message)
        {
            await _ticketRepository.AddAsync(message);
        }

        public async Task ChangeStatusById(Guid id, Status status)
        {
            await _ticketRepository.ChangeStatusById(id, status);
        }
    }
}
