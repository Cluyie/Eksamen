using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.User.Domain.Commands;
using UCLDreamTeam.User.Domain.Events;
using UCLDreamTeam.User.Domain.Interface;

namespace UCLDreamTeam.User.Domain.CommandHandlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IEventBus _eventBus;
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IEventBus eventBus, IUserRepository userRepository)
        {
            _eventBus = eventBus;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.CreateUserAsync(request.User);
            if (result.Succeeded)
            {
                _eventBus.PublishEvent(new UserCreatedEvent(request.User, request.Role));
            }
            else
                _eventBus.PublishEvent(new UserRejectedEvent(request.User, result.Errors));

            return true;
        }
    }
}
