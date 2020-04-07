using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.User.Domain.Commands;
using UCLDreamTeam.User.Domain.Events;

namespace UCLDreamTeam.User.Domain.CommandHandlers
{
    public class RegisterUserRejectedCommandHandler : IRequestHandler<RegisterUserRejectedCommand, bool>
    {
        private readonly IEventBus _eventBus;

        public RegisterUserRejectedCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task<bool> Handle(RegisterUserRejectedCommand request, CancellationToken cancellationToken)
        {
            _eventBus.PublishEvent(new UserRejectedEvent(request.User));
            return Task.FromResult(true);
        }
    }
}
