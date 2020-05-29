using System;
using System.Threading.Tasks;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Auth.Api.Infrastructure;
using UCLDreamTeam.Auth.Api.Infrastructure.Services;
using UCLDreamTeam.Auth.Api.IntegrationEvents.Events;
using UCLDreamTeam.Auth.Api.Models;
using UCLDreamTeam.Auth.Api.Models.DTO;

namespace UCLDreamTeam.Auth.Api.IntegrationEvents.EventHandlers
{
    public class UserDeletedEventHandler : IEventHandler<UserDeletedEvent>
    {
        private readonly AuthRepository _authRepository;

        public UserDeletedEventHandler(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task Handle(UserDeletedEvent @event)
        {
            AuthUser user = await _authRepository.GetUserFromId(@event.User.Id);

            await _authRepository.Delete(user);
        }
    }
}
