using RabbitMQ.Bus.Bus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCLDreamTeam.Auth.Api.Infrastructure;
using UCLDreamTeam.Auth.Api.IntegrationEvents.Events;
using UCLDreamTeam.Auth.Api.Models;

namespace UCLDreamTeam.Auth.Api.IntegrationEvents.EventHandlers
{
    public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
    {
        private readonly AuthRepository _authRepository;

        public UserCreatedEventHandler(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        async Task IEventHandler<UserCreatedEvent>.Handle(UserCreatedEvent @event)
        {
            await _authRepository.Create(@event.User);
        }
    }
}
