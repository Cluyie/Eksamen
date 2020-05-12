using System;

namespace UCLDreamTeam.User.Domain.Events
{
    public class UserDeleteFailedEvent 
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