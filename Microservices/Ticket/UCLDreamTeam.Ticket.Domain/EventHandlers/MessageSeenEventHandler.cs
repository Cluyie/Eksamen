using System.Threading.Tasks;
using RabitMQEasy;
using UCLDreamTeam.Ticket.Domain.Events;
using UCLDreamTeam.Ticket.Domain.Interfaces;

namespace UCLDreamTeam.Ticket.Domain.EventHandlers
{
    public class MessageSeenEventHandler : ILissener<MessageSeenEvent>
    {
        private readonly ITicketRepository _ticketRepository;

        public MessageSeenEventHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task action(MessageSeenEvent Obj)
        {
            await _ticketRepository.MessageSeen(Obj.MessageId, Obj.Seen);
        }
    }
}
