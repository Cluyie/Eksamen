using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Bus.Events;
using UCLDreamTeam.Auth.Api.Models;
using UCLDreamTeam.Auth.Api.Models.DTO;

namespace UCLDreamTeam.Auth.Api.IntegrationEvents.Events
{
    public class UserCreatedEvent : Event
    {
        public CreateUserCredentialsDTO User { get; set; }
        public RoleDTO Role { get; set; }

        public UserCreatedEvent(CreateUserCredentialsDTO user, RoleDTO role = null)
        {
            User = user;
            Role = role ?? new RoleDTO { RoleName = "User" };
        }
    }
}
