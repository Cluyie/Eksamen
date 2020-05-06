using RabbitMQ.Bus.Bus.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces;
using UCLDreamTeam.Ticket.Domain.Interfaces;
using UCLDreamTeam.Ticket.Domain.Models;
using UCLDreamTeam.Ticket.Domain.Models.Event;

namespace UCLDreamTeam.Ticket.Api.EventHandlers
{
    public class ConectionCreadetEventHandler : IEventHandler<Conection>
    {
        ITicketService TicketService { get; set; }
        public ConectionCreadetEventHandler(ITicketService ticketService)
        {
            TicketService = ticketService;
        }
        public async Task Handle(Conection @event)
        {
            await TicketService.AddAsync(new Domain.Models.Ticket()
            {
                Id = @event.GroopId,
                Status = Status.Active,
                Description = @event.Description,
                ResourceId = @event.ResourceId,
                UserTickets =
                new List<UserTicket>
                {
                    new UserTicket{UserId = @event.KundeId, TicketId = @event.GroopId},
                    new UserTicket{UserId = @event.SuportId, TicketId = @event.GroopId}
                }
            });
        }
    }
}
