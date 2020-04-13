using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.User.Domain.Commands;
using UCLDreamTeam.User.Domain.Events;

namespace UCLDreamTeam.User.Domain.CommandHandlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IEventBus _eventBus;
        private readonly UserManager<Models.User> _userManager;

        public RegisterUserCommandHandler(IEventBus eventBus, UserManager<Models.User> userManager)
        {
            _eventBus = eventBus;
            _userManager = userManager;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateAsync(request.User);
            if (result.Succeeded)
            {
                _eventBus.PublishEvent(new UserRegisteredEvent(request.User));
            }
            else
                _eventBus.PublishEvent(new UserRejectedEvent(request.User, result.Errors));

            return true;
        }
    }
}
