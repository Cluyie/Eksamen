using MediatR;
using RabbitMQ.Bus.Bus.Interfaces;
using SignalR_Microservice.Commands;
using SignalR_Microservice.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SignalR_Microservice.CommandHandlers
{
    public class CreateSentMessageCommandHandler : IRequestHandler<CreateSentMessageCommand, bool>
    {
        private readonly IEventBus _eventBus;

        public CreateSentMessageCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task<bool> Handle(CreateSentMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sentMessage = new Models.Message
                {
                    Id = request.Id,
                    Text = request.Text,
                    GroopId = request.TicketId,
                    TimeStamp = request.TimeStamp,
                };

                _eventBus.PublishEvent(new MessageSentEvent(sentMessage));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return await Task.FromResult(true);
        }
    }
}