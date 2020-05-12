using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace UCLDreamTeam.User.Domain.Events
{
    public class UserRejectedEvent 
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