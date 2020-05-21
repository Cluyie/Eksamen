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
    public class UserUpdatedEventHandler : IEventHandler<UserUpdatedEvent>
    {
        private readonly HashService _hashService;
        private readonly AuthRepository _authRepository;

        public UserUpdatedEventHandler(HashService hashService, AuthRepository authRepository)
        {
            _hashService = hashService;
            _authRepository = authRepository;
        }

        async Task IEventHandler<UserUpdatedEvent>.Handle(UserUpdatedEvent @event)
        {
            UpdateUserCredentialsDTO userIn = @event.User;
            AuthUser userToUpdate = await _authRepository.GetUserFromId(userIn.Id);

            userToUpdate.UserName = userIn.UserName;
            userToUpdate.Email = userIn.Email;

            userToUpdate.PasswordHash = _hashService.GenerateHash(userToUpdate.PasswordHash, userToUpdate.PasswordSalt);

            await _authRepository.UpdateUser(userToUpdate);

            await Task.CompletedTask;
        }
    }
}
