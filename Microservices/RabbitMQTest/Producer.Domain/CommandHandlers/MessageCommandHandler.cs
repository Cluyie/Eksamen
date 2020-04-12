using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Producer.Domain.Commands;
using Producer.Domain.Events;
using RabbitMQ.Bus.Bus.Interfaces;

namespace Producer.Domain.CommandHandlers
{
    public class MessageCommandHandler : IRequestHandler<CreateMessageCommand, bool>
    {
        private readonly IEventBus _eventBus;

        public MessageCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task<bool> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            _eventBus.PublishEvent(new MessageCreatedEvent(request.Message));
            return Task.FromResult(true);
        }
    }
}