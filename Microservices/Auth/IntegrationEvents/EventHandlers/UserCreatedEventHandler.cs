using RabitMQEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCLDreamTeam.Auth.Api.Infrastructure;
using UCLDreamTeam.Auth.Api.Infrastructure.Services;
using UCLDreamTeam.Auth.Api.Models;
using UCLDreamTeam.Auth.Api.Models.DTO;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Auth.Api.IntegrationEvents.EventHandlers
{
    public class UserCreatedEventHandler : IEventHandler<CreateUserCredentialsDTO, IUser>
    {
        private readonly AuthRepository _authRepository;
        private readonly HashService _hashService;
        public Events events { get; set; } 

        public UserCreatedEventHandler(AuthRepository authRepository, HashService hashService)
        {
            _authRepository = authRepository;
            _hashService = hashService;
            events = Events.NewObject;
        }


        public async Task action(IUser Obj)
        {
            CreateUserCredentialsDTO userIn = (CreateUserCredentialsDTO)Obj;
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
