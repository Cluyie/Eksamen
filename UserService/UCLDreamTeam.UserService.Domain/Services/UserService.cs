﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Models.Interfaces;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.UserServiceApi.Domain.Commands;
using UCLDreamTeam.UserServiceApi.Domain.Context;
using UCLDreamTeam.UserServiceApi.Domain.Models;
using UCLDreamTeam.UserServiceApi.Domain.Services.Interfaces;

namespace UCLDreamTeam.UserServiceApi.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IdentityContext _identityContext;
        private readonly Mapper _mapper;
        private readonly IEventBus _eventBus;

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
            if (result.Succeeded)
            {
                await _eventBus.SendCommand(new RegisterUserCommand(user));
                return new ApiResponse<string>(ApiResponseCode.OK, "");
            }
            await _eventBus.SendCommand(new RegisterUserRejectedCommand(user));
            return new ApiResponse<string>(ApiResponseCode.InternalServerError, "");
        }

        public async Task<ApiResponse<User>> Update(User userData)
        {
            //Prevent changing the ID
            userData.Id = Guid.Empty;
            User userToChange = GetUserFromIdAsync(userData.Id).Result;
            // Can only update an existing user
            if (userToChange == null)
            {
                await _eventBus.SendCommand(new NoUserFoundCommand(userToChange));
                return new ApiResponse<User>(ApiResponseCode.NotFound, null);
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