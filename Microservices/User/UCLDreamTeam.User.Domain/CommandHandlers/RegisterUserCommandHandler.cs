using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.User.Domain.Commands;
using UCLDreamTeam.User.Domain.Events;

namespace UCLDreamTeam.User.Domain.CommandHandlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IEventBus _eventBus;

        public RegisterUserCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            _eventBus.PublishEvent(new UserRegisteredEvent(request.User));
            return Task.FromResult(true);
        }
    }
}
