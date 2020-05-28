using RabbitMQ.Bus.Bus.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UCLDreamTeam.Ticket.Domain.Interfaces;

namespace UCLDreamTeam.Ticket.Domain.EventHandlers
{
    public class UserDeletedEventHandler : IEventHandler<UserDeletedEvent>
    {
        private ITicketRepository _ticketRespitory;
        public UserDeletedEventHandler(ITicketRepository ticketRespitory)
        {
            _ticketRespitory = ticketRespitory;
        }

        public async Task Handle(UserDeletedEvent @event)
        {
            await _ticketRespitory.DeleteUserAsync(@event.User);
        }
    }
}
