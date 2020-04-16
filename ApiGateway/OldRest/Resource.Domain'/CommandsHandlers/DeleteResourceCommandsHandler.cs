using MediatR;
using RabbitMQ.Bus.Commands;
using Resource.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Domain.InternealCommands
{
    public class DeleteResourceCommandsHandler : IRequestHandler<CreatResourceCommand, bool>
    {
        public Task<bool> Handle(CreatResourceCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
