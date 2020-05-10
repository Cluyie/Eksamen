using RabbitMQ.Bus.Bus.Interfaces;
using RabitMQEasy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLDreamTeam.Ticket.Domain.Events;
using UCLDreamTeam.Ticket.Domain.Interfaces;

namespace UCLDreamTeam.Ticket.Domain.EventHandlers
{
    public class ConectionCreadetEventHandler : IEventHandler<Models.Event.Conection, IConection>
    {
        private readonly ITicketRepository _ticketRepository;
        public RabitMQEasy.Events events { get ; set; }
        public ConectionCreadetEventHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
            events = RabitMQEasy.Events.NewObject;
        }


        public async Task action(IConection Obj)
        {
            IConection c = Obj;
            await _ticketRepository.AddAsync(new Models.Ticket() { Description = c.Description, Name = c.Name, ResourceId = c.ResourceId, Id = c.GroopId, Status = SharedInterfaces.Status.Active, Messages = new List<Models.Message>(), UserTickets = new List<Models.UserTicket>()});
        }
    }
}
