using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.User.Domain.Commands;
using UCLDreamTeam.User.Domain.Events;
using UCLDreamTeam.User.Domain.Interface;

namespace UCLDreamTeam.User.Domain.CommandHandlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IEventBus _eventBus;
        private readonly IUserRepository _userRepository;
        private readonly Mapper _mapper;

        public UpdateUserCommandHandler(IEventBus eventBus, IUserRepository userRepository, Mapper mapper)
        {
            _eventBus = eventBus;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Finds the user
                var dbUser = await _userRepository.GetUserAsync(request.InputUser.Id);

                if (dbUser == null)
                {
                    _eventBus.PublishEvent(new NoUserFoundEvent(request.UserToChange));
                    return false;
                }

                await _userRepository.UpdateUserAsync(request.InputUser, dbUser);

                _eventBus.PublishEvent(new UserUpdatedEvent(dbUser, request.Role));
                return true;
            }
            catch (Exception e)
            {
                _eventBus.PublishEvent(new UserUpdateFailedEvent(request.UserToChange, e));
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
