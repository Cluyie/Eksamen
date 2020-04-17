using MediatR;
using RabbitMQ.Bus.Commands;
using Resource.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Domain.InternealCommands
{
    public class UpdateRecourceCommandsHandler : IRequestHandler<UpdateRecourceCommand, bool>
    {
        public Task<bool> Handle(UpdateRecourceCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
