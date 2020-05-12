using System;

namespace UCLDreamTeam.User.Domain.Events
{
    public class UserUpdateFailedEvent
    {
        public Models.User User { get; }
        public Exception Exception { get; }

        public UserUpdateFailedEvent(Models.User user, Exception exception)
        {
            User = user;
            Exception = exception;
        }
    }
}