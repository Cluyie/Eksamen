using RabbitMQ.Bus.Events;

namespace UCLDreamTeam.User.Domain.Events
{
    public class UserRejectedEvent : Event
    {
        public Models.User User { get; set;  }

        public UserRejectedEvent(Models.User user)
        {
            User = user;
        }
    }
}