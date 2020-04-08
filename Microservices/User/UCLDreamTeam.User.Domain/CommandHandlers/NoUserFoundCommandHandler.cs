using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.User.Domain.Commands;
using UCLDreamTeam.User.Domain.Events;

namespace UCLDreamTeam.User.Domain.CommandHandlers
{
    public class NoUserFoundCommandHandler : IRequestHandler<NoUserFoundCommand, bool>
    {
        private readonly IEventBus _eventBus;

        public NoUserFoundCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task<bool> Handle(NoUserFoundCommand request, CancellationToken cancellationToken)
        {
            _eventBus.PublishEvent(new NoUserFoundEvent(request.User));
            return Task.FromResult(true);
        }
    }
}
