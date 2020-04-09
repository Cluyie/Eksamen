using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Models.Interfaces;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.UserServiceApi.Domain.Context;
using UCLDreamTeam.UserServiceApi.Domain.Models;
using UCLDreamTeam.UserServiceApi.Domain.Services.Interfaces;

namespace UCLDreamTeam.UserServiceApi.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IEventBus _eventBus;
        private readonly IdentityContext _identityContext;
        private readonly Mapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager,
            IdentityContext identityContext, IEventBus eventBus)
        {
            _userManager = userManager;
            _identityContext = identityContext;
            _eventBus = eventBus;
        }

        public async Task<ApiResponse<string>> RegisterAsync(User userToRegister)
        {
            var user = userToRegister;
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded) return new ApiResponse<string>(ApiResponseCode.OK, "");
            return new ApiResponse<string>(ApiResponseCode.InternalServerError, "");
        }

        public ApiResponse<User> Update(User userData)
        {
            //Prevent changing the ID
            userData.Id = Guid.Empty;
            var userToChange = GetUserFromIdAsync(userData.Id).Result;
            // Can only update an existing user
            if (userToChange == null) return new ApiResponse<User>(ApiResponseCode.UnAuthenticated, null);

            // Update the user
            if (!string.IsNullOrWhiteSpace(userData.PasswordHash) && userData.PasswordHash != userToChange.PasswordHash)
                //If the password is unchanged or empty, this does not update the password
                userData.PasswordHash = userToChange.PasswordHash;
            // Automapper is configured to only overwrite the fields that are not null
            _mapper.Map(userData, userToChange);

            _identityContext.Update(userToChange);
            _identityContext.SaveChanges();

            return new ApiResponse<User>(ApiResponseCode.OK, userToChange);
        }

        // ----- Internal methods -----

        public async Task<User> GetUserFromIdAsync(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<User> GetUserFromUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
    }
}