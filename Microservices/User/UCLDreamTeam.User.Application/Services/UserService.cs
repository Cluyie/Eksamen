using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Models.Interfaces;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.User.Application.Interfaces;
using UCLDreamTeam.User.Data.Context;
using UCLDreamTeam.User.Domain.Commands;
using UCLDreamTeam.User.Domain.Models;

namespace UCLDreamTeam.User.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<Domain.Models.User> _userManager;
        private readonly IdentityContext _identityContext;
        private readonly Mapper _mapper;
        private readonly IEventBus _eventBus;

        public UserService(UserManager<Domain.Models.User> userManager,
            IdentityContext identityContext, IEventBus eventBus)
        {
            _userManager = userManager;
            _identityContext = identityContext;
            _eventBus = eventBus;
        }

        public async Task<ApiResponse<string>> RegisterAsync(Domain.Models.User userToRegister)
        {
            var user = userToRegister;
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await _eventBus.SendCommand(new RegisterUserCommand(user));
                return new ApiResponse<string>(ApiResponseCode.OK, "");
            }
            await _eventBus.SendCommand(new RegisterUserRejectedCommand(user));
            return new ApiResponse<string>(ApiResponseCode.InternalServerError, "");
        }

        public async Task<ApiResponse<Domain.Models.User>> Update(Domain.Models.User userData)
        {
            //Prevent changing the ID
            userData.Id = Guid.Empty;
            Domain.Models.User userToChange = GetUserFromIdAsync(userData.Id).Result;
            // Can only update an existing user
            if (userToChange == null)
            {
                await _eventBus.SendCommand(new NoUserFoundCommand(userToChange));
                return new ApiResponse<Domain.Models.User>(ApiResponseCode.NotFound, null);
            }

            // Update the user
            if (!string.IsNullOrWhiteSpace(userData.PasswordHash) && userData.PasswordHash != userToChange.PasswordHash)
            {
                //If the password is unchanged or empty, this does not update the password
                userData.PasswordHash = userToChange.PasswordHash;
            }
            // Automapper is configured to only overwrite the fields that are not null
            _mapper.Map(userData, userToChange);

            _identityContext.Update(userToChange);
            _identityContext.SaveChanges();

            await _eventBus.SendCommand(new UpdateUserCommand(userToChange));
            return new ApiResponse<Domain.Models.User>(ApiResponseCode.OK, userToChange);
        }

        // ----- Internal methods -----

        public async Task<Domain.Models.User> GetUserFromIdAsync(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<Domain.Models.User> GetUserFromUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
    }
}