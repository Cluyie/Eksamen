using RabbitMQ.Bus.Bus.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UCLDreamTeam.Ticket.Domain.Interfaces;

namespace UCLDreamTeam.Ticket.Domain.EventHandlers
{
    public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
    {
        private ITicketRepository _ticketRespitory;

        public UserCreatedEventHandler(ITicketRepository ticketRespitory)
        {
            _ticketRespitory = ticketRespitory;
        }

        public async Task Handle(UserCreatedEvent @event)
        {
            await _ticketRespitory.AddUserAsync(@event.User);
        }
    }
}
