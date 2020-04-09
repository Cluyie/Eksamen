using RabbitMQ.Bus.Events;
using UCLDreamTeam.UserServiceApi.Domain.Models;

namespace UCLDreamTeam.UserServiceApi.Domain.Events
{
    public class UserUpdatedEvent : Event
    {
        public UserUpdatedEvent(User user)
        {
            User = user;
        }

        public User User { get; set; }
    }
}