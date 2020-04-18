using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using RabbitMQ.Bus.Commands;
using UCLDreamTeam.Resource.Domain.InternalCommands;

namespace UCLDreamTeam.Resource.Domain.CommandsHandlers
{
    public class DeleteResourceCommandsHandler : IRequestHandler<CreateResourceCommand, bool>
    {
        public Task<bool> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
