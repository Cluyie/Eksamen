using RabbitMQ.Bus.Bus.Interfaces;
using System;
using System.Threading.Tasks;
using UCLDreamTeam.User.Application.Interfaces;
using UCLDreamTeam.User.Domain.Commands;
using UCLDreamTeam.User.Domain.Interface;
using UCLDreamTeam.User.Domain.Models;

namespace UCLDreamTeam.User.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventBus _eventBus;

        public UserService(IUserRepository userRepository, IEventBus eventBus)
        {
            _userRepository = userRepository;
            _eventBus = eventBus;
        }

        public async Task RegisterAsync(Domain.Models.User userToRegister, Role userRole = null)
        {
            await _eventBus.SendCommand(new RegisterUserCommand(userToRegister, userRole));
        }

        public async Task<Domain.Models.User> Update(Domain.Models.User userData, Role userRole = null)
        {
            await _eventBus.SendCommand(new UpdateUserCommand(userData,
                await _userRepository.GetUserAsync(userData.Id), userRole));
            return userData;
        }

        public async Task DeleteUserFromIdAsync(Guid id)
        {
            var command = new DeleteUserCommand(await GetUserFromIdAsync(id));
            await _eventBus.SendCommand(command);
        }

        // ----- Internal methods -----

        public async Task<Domain.Models.User> GetUserFromIdAsync(Guid id)
        {
            return await _userRepository.GetUserAsync(id);
        }

        public async Task<Domain.Models.User> GetUserFromUserNameAsync(string userName)
        {
            return await _userRepository.GetFromUserNameAsync(userName);
        }
    }
}
