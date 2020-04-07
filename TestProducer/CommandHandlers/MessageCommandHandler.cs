using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RabbitMQ.Bus.Bus.Interfaces;
using TestProducer.Commands;
using TestProducer.Events;

namespace TestProducer.CommandHandlers
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