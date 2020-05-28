using RabbitMQ.Bus.Events;
using UCLDreamTeam.User.Domain.Models;

namespace UCLDreamTeam.User.Domain.Events
{
    public class UserUpdatedEvent : Event
    {
        public Models.User User { get; set;  }
        public Role Role { get; set; }

        public UserUpdatedEvent(Models.User user, Role role = null)
        {
            User = user;
            Role = role;
        }
    }
}