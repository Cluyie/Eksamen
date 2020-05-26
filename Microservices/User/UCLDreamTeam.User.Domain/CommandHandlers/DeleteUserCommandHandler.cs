using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.User.Domain.Commands;
using UCLDreamTeam.User.Domain.Interface;
using UCLDreamTeam.User.Domain.Events;

namespace UCLDreamTeam.User.Domain.CommandHandlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IEventBus _eventBus;
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IEventBus eventBus, IUserRepository userRepository)
        {
            _eventBus = eventBus;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _userRepository.DeleteUserAsync(request.User);
                _eventBus.PublishEvent(new UserDeletedEvent(request.User));
                return true;
            }
            catch (Exception e)
            {
#if DEBUG
                Console.WriteLine(e);
                throw e;
#else
                _eventBus.PublishEvent(new UserDeleteFailedEvent(request.User) {Exception = e});
                return false;
#endif
            }
        }
    }
}
