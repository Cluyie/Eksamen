using RabbitMQ.Bus.Events;

namespace UCLDreamTeam.User.Domain.Events
{
    public class UserDeletedEvent : Event
    {
        public Models.User User { get; set;  }

        public UserDeletedEvent(Models.User user)
        {
            User = user;
        }
    }
}