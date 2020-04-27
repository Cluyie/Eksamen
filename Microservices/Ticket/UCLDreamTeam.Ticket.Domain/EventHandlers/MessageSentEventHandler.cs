using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Bus.Bus.Interfaces;
using RabbitMQ.Bus.Events;
using UCLDreamTeam.Ticket.Domain.Events;
using UCLDreamTeam.Ticket.Domain.Interfaces;

namespace UCLDreamTeam.Ticket.Domain.EventHandlers
{
    public class MessageSentEventHandler : IEventHandler<MessageSentEvent>
    {
        private readonly ITicketRepository _ticketRepository;

        public MessageSentEventHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task Handle(MessageSentEvent @event)
        {
            await _ticketRepository.AddMessageAsync(@event.Message);
        }
    }
}
