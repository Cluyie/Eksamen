using RabbitMQ.Bus.Events;

namespace UCLDreamTeam.User.Domain.Events
{
    public class UserCreatedEvent : Event
    {
        public Models.User User { get; set;  }

        public UserCreatedEvent(Models.User user)
        {
            User = user;
        }
    }
}