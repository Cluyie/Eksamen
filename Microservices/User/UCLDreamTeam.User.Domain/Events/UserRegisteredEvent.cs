using RabbitMQ.Bus.Events;

namespace UCLDreamTeam.User.Domain.Events
{
    public class UserRegisteredEvent : Event
    {
        public Models.User User { get; set;  }

        public UserRegisteredEvent(Models.User user)
        {
            User = user;
        }
    }
}