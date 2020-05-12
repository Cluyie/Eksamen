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
    public class UserDeletedEventHandler : IEventHandler<CreateUserCredentialsDTO, IUser>
    {
        private readonly AuthRepository _authRepository;

        public UserDeletedEventHandler(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public Events events { get; set; } = Events.DeleateObject;

        public async Task action(IUser Obj)
        {
            AuthUser user = await _authRepository.GetUserFromId(Obj.Id);

            await _authRepository.Delete(user);
        }
    }
}
