using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCLDreamTeam.Auth.Api.Models;
using UCLDreamTeam.Auth.Api.Models.DTO;

namespace UCLDreamTeam.Auth.Api.IntegrationEvents.Events
{
    public class UserCreatedEvent
    {
        public AuthUser User { get; set; }

        public UserCreatedEvent(AuthUser user)
        {
            User = user;
        }
    }
}
