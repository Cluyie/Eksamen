using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using RabbitMQ.Bus.Events;

namespace UCLDreamTeam.User.Domain.Events
{
    public class UserRejectedEvent : Event
    {
        public Models.User User { get; }
        public IEnumerable<IdentityError> ResultErrors { get; }

        public UserRejectedEvent(Models.User user, IEnumerable<IdentityError> resultErrors)
        {
            User = user;
            ResultErrors = resultErrors;
        }
    }
}