using System;
using System.Threading.Tasks;
using RabitMQEasy;
using UCLDreamTeam.Auth.Api.Infrastructure;
using UCLDreamTeam.Auth.Api.Infrastructure.Services;
using UCLDreamTeam.Auth.Api.Models;
using UCLDreamTeam.Auth.Api.Models.DTO;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Auth.Api.IntegrationEvents.EventHandlers
{
    public class UserUpdatedEventHandler : IEventHandler<CreateUserCredentialsDTO, IUser>
    {
        private readonly HashService _hashService;
        private readonly AuthRepository _authRepository;

        public UserUpdatedEventHandler(HashService hashService, AuthRepository authRepository)
        {
            _hashService = hashService;
            _authRepository = authRepository;
        }

        public Events events { get; set; } = Events.UpdateObject;

        public async Task action(IUser Obj)
        {
            CreateUserCredentialsDTO userIn = (CreateUserCredentialsDTO)Obj;
            AuthUser userToUpdate = await _authRepository.GetUserFromId(userIn.Id);

            userToUpdate.UserName = userIn.UserName;
            userToUpdate.Email = userIn.Email;

            userToUpdate.PasswordHash = _hashService.GenerateHash(userToUpdate.PasswordHash, userToUpdate.PasswordSalt);

            await _authRepository.UpdateUser(userToUpdate);

            await Task.CompletedTask;
        }
    }
}
