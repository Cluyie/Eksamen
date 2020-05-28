using System;
using RabbitMQ.Bus.Events;
using UCLDreamTeam.Auth.Api.Models;
using UCLDreamTeam.Auth.Api.Models.DTO;

namespace UCLDreamTeam.Auth.Api.IntegrationEvents.Events
{
    public class UserUpdatedEvent : Event
    {
        public UpdateUserCredentialsDTO User { get; set; }
        public RoleDTO Role { get; set; }

        public UserUpdatedEvent(UpdateUserCredentialsDTO user, RoleDTO role = null)
        {
            User = user;
            Role = role ?? new RoleDTO { RoleName = "User" };
        }
    }
}
