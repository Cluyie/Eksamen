using MediatR;
using RabbitMQ.Bus.Commands;
using System.Threading;
using System.Threading.Tasks;
using UCLDreamTeam.Resource.Domain.InternalCommands;

namespace UCLDreamTeam.Resource.Domain.CommandsHandlers
{
    public class UpdateResourceCommandsHandler : IRequestHandler<UpdateResourceCommand, bool>
    {
        public Task<bool> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
