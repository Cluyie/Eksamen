using RabbitMQ.Bus.Events;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLDreamTeam.Ticket.Domain.Models.Event;

namespace UCLDreamTeam.Ticket.Domain.Events
{
    public class ConectionCreadetEvent
    {
        public IConection Conection { get; set; }

        public ConectionCreadetEvent(Conection conection)
        {
            Conection = conection;
        }
    }
}