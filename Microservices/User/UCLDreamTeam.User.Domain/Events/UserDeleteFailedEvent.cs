using System;
using RabbitMQ.Bus.Events;

namespace UCLDreamTeam.User.Domain.Events
{
    public class UserDeleteFailedEvent : Event
    {
        public Models.User User { get; }
        public Exception Exception { get; }

        public UserDeleteFailedEvent(Models.User user, Exception exception)
        {
            User = user;
            Exception = exception;
        }
    }
}