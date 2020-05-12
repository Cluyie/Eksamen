using RabitMQEasy;
using System;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLDreamTeam.User.Application.Interfaces;
using UCLDreamTeam.User.Domain.Interface;

namespace UCLDreamTeam.User.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly RabitMQPublicer _eventBus;

        public UserService(IUserRepository userRepository, RabitMQPublicer eventBus)
        {
            _userRepository = userRepository;
            _eventBus = eventBus;
        }

        public async Task RegisterAsync(Domain.Models.User userToRegister)
        {
            var result = await _userRepository.CreateUserAsync(userToRegister);
            if (result.Succeeded)
            {
                _eventBus.PunlicEvent<IUser>(Events.NewObject, userToRegister);
            }
            else
            {
                _eventBus.PunlicEvent<IUser>(Events.NewObject, userToRegister);
            }
        }

        public async Task<Domain.Models.User> Update(Domain.Models.User userData)
        {
            var dbUser = await _userRepository.GetUserAsync(userData.Id);
            try
            {
                // Finds the user


                await _userRepository.UpdateUserAsync(userData, dbUser);

                _eventBus.PunlicEvent<IUser>(Events.UpdateObject , await _userRepository.GetUserAsync(userData.Id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return dbUser;
        }

        public async Task DeleteUserFromIdAsync(Guid id)
        {
            var command = await GetUserFromIdAsync(id);
            try
            {
                await _userRepository.DeleteUserAsync(command);
                _eventBus.PunlicEvent<IUser>(Events.DeleateObject, command);
            }
            catch (Exception e)
            {
#if DEBUG
                Console.WriteLine(e);
                throw e;
#else
                _eventBus.PublishEvent(new UserDeleteFailedEvent(request.User, e));
                return false;
#endif
            }
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