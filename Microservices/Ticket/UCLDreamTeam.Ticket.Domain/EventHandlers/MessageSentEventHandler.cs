using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Bus.Events;
using RabitMQEasy;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLDreamTeam.Ticket.Domain.Events;
using UCLDreamTeam.Ticket.Domain.Interfaces;

namespace UCLDreamTeam.Ticket.Domain.EventHandlers
{
    public class MessageSentEventHandler : IEventHandler<Models.Message, IMessage>
    {
        private readonly ITicketRepository _ticketRepository;
        public RabitMQEasy.Events events { get; set; }
        public MessageSentEventHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public async Task action(IMessage Obj)
        {
            await _ticketRepository.AddMessageAsync((Models.Message)Obj);
        }
    }
}
