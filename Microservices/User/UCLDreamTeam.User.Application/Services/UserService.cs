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
        private readonly IdentityDbContext _identityDbContext;
        private readonly Mapper _mapper;
        private readonly IEventBus _eventBus;

        public UserService(UserManager<Domain.Models.User> userManager,
            IdentityDbContext identityDbContext, IEventBus eventBus)
        {
            _userManager = userManager;
            _identityDbContext = identityDbContext;
            _eventBus = eventBus;
        }

        public async Task RegisterAsync(Domain.Models.User userToRegister)
        {
            await _eventBus.SendCommand(new RegisterUserCommand(userToRegister));
        }

        public async Task<Domain.Models.User> Update(Domain.Models.User userData)
        {
            await _eventBus.SendCommand(new UpdateUserCommand(userData,
                await _userManager.FindByIdAsync(userData.Id.ToString())));
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
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<Domain.Models.User> GetUserFromUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
    }
}