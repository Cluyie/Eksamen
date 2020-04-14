using RabbitMQ.Bus.Events;

namespace UCLDreamTeam.User.Domain.Events
{
    public class UserUpdatedEvent : Event
    {
        public Models.User User { get; set;  }

        public UserUpdatedEvent(Models.User user)
        {
            User = user;
        }
    }
}