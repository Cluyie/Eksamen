using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.UserServiceApi.Domain.Commands;

namespace UCLDreamTeam.UserServiceApi.Domain.CommandHandlers
{
    public class NoUserFoundCommandHandler : IRequestHandler<NoUserFoundCommand, bool>
    {
        public Task<bool> Handle(NoUserFoundCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
