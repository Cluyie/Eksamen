using RabbitMQ.Bus.Bus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCLDreamTeam.Auth.Api.Infrastructure;
using UCLDreamTeam.Auth.Api.Infrastructure.Services;
using UCLDreamTeam.Auth.Api.IntegrationEvents.Events;
using UCLDreamTeam.Auth.Api.Models;
using UCLDreamTeam.Auth.Api.Models.DTO;

namespace UCLDreamTeam.Auth.Api.IntegrationEvents.EventHandlers
{
    public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
    {
        private readonly AuthRepository _authRepository;
        private readonly HashService _hashService;

        public UserCreatedEventHandler(AuthRepository authRepository, HashService hashService)
        {
            _authRepository = authRepository;
            _hashService = hashService;
        }

        async Task IEventHandler<UserCreatedEvent>.Handle(UserCreatedEvent @event)
        {
            CreateUserCredentialsDTO userIn = @event.User;
            string salt = _hashService.GenerateSalt();
            AuthUser userToCreate = new AuthUser
            {
                Id = userIn.Id,
                UserName = userIn.UserName,
                Email = userIn.Email,
                PasswordSalt = salt,
                PasswordHash = _hashService.GenerateHash(userIn.PasswordHash, salt)
            };

            await _authRepository.Create(userToCreate);

            await Task.CompletedTask;
        }
    }
}
