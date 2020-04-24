using System;
using RabbitMQ.Bus.Events;
using UCLDreamTeam.Auth.Api.Models;
using UCLDreamTeam.Auth.Api.Models.DTO;

namespace UCLDreamTeam.Auth.Api.IntegrationEvents.Events
{
    public class UserDeletedEvent : Event
    {
        public DeletedUserDTO User { get; set; }

        public UserDeletedEvent(DeletedUserDTO user)
        {
            User = user;
        }
    }
}
