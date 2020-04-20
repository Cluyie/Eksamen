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
    public class MessageSeenEventHandler : IEventHandler<MessageSeenEvent>
    {
        private readonly ITicketRepository _ticketRepository;

        public MessageSeenEventHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public Task Handle(MessageSeenEvent @event)
        {
            _ticketRepository.MessageSeen(@event.MessageId, @event.Seen);
        }
    }
}
