using System.Threading.Tasks;
using RabbitMQ.Bus.Bus.Interfaces;
using RabitMQEasy;
using UCLDreamTeam.Ticket.Domain.Events;
using UCLDreamTeam.Ticket.Domain.Interfaces;

namespace UCLDreamTeam.Ticket.Domain.EventHandlers
{
    public class TicketCreatedEventHandler : ILissener<TicketCreatedEvent>
    {
        private readonly ITicketService _ticketService;

        public TicketCreatedEventHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task action(TicketCreatedEvent Obj)
        {
            await _ticketService.AddAsync(Obj.Ticket);
        }
    }
}
