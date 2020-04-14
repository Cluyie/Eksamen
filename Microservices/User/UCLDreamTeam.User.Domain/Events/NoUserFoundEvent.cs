using RabbitMQ.Bus.Events;

namespace UCLDreamTeam.User.Domain.Events
{
    public class NoUserFoundEvent : Event
    {
        public Models.User User { get; set;  }

        public NoUserFoundEvent(Models.User user)
        {
            User = user;
        }
    }
}