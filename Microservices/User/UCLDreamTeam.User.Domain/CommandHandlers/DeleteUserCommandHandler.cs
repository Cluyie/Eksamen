using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.User.Domain.Commands;
using UCLDreamTeam.User.Domain.Events;

namespace UCLDreamTeam.User.Domain.CommandHandlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IEventBus _eventBus;
        private readonly UserManager<Models.User> _userManager;

        public DeleteUserCommandHandler(IEventBus eventBus, UserManager<Models.User> userManager)
        {
            _eventBus = eventBus;
            _userManager = userManager;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _userManager.DeleteAsync(request.User);
                _eventBus.PublishEvent(new UserDeletedEvent(request.User));
                return true;
            }
            catch (Exception e)
            {
                _eventBus.PublishEvent(new UserDeleteFailedEvent(request.User, e));
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
