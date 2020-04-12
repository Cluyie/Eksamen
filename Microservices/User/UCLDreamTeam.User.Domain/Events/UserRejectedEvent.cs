using RabbitMQ.Bus.Events;

namespace UCLDreamTeam.User.Domain.Events
{
    public class UserRejectedEvent : Event
    {
        public Models.User User { get; }

        public UserRejectedEvent(Models.User user)
        {
            User = user;
        }
    }
}