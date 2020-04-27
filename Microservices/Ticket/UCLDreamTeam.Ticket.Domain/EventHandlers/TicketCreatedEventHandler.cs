using System.Threading.Tasks;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Ticket.Domain.Events;
using UCLDreamTeam.Ticket.Domain.Interfaces;

namespace UCLDreamTeam.Ticket.Domain.EventHandlers
{
    public class TicketCreatedEventHandler : IEventHandler<TicketCreatedEvent>
    {
        private readonly ITicketService _ticketService;

        public TicketCreatedEventHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task Handle(TicketCreatedEvent @event)
        {
            await _ticketService.AddAsync(@event.Ticket);

        }
    }
}
