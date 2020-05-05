using RabbitMQ.Bus.Bus.Interfaces;
using RabbitMQ.Bus.Events;
using System.Threading.Tasks;
using UCLDreamTeam.Ticket.Domain.Interfaces;
using UCLDreamTeam.Ticket.Domain.Models;
using UCLDreamTeam.Ticket.Domain.Models.Event;

namespace UCLDreamTeam.Ticket.Api.EventHandlers
{
    public class MessageSentEventHandler : IEventHandler<MessageEvent>
    {
        ITicketService TicketService { get; set; }
        public MessageSentEventHandler(ITicketService ticketService)
        {
            TicketService = ticketService;
        }
        public async Task Handle(MessageEvent @event)
        {
            await TicketService.AddMessageAsync(new Domain.Models.Message() 
                { GroopId = @event.GroopId, Seen = @event.Seen, Text = @event.Text, TimeStamp = @event.TimeStamp, UserId = @event.UserId });
        }
    }
}
