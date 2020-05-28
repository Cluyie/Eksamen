using RabbitMQ.Bus.Events;
using UCLDreamTeam.Ticket.Domain.Models;

namespace UCLDreamTeam.Ticket.Domain.EventHandlers
{
    public class UserDeletedEvent : Event
    {
        public User User { get; set; }
        public Role Role { get; set; }

        public UserDeletedEvent(User user, Role role)
        {
            User = user;
            Role = role;
        }
    }
}