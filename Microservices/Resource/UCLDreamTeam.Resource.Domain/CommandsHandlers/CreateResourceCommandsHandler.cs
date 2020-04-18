using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UCLDreamTeam.Resource.Domain.InternalCommands;

namespace UCLDreamTeam.Resource.Domain.CommandsHandlers
{
    public class CreateResourceCommandsHandler : IRequestHandler<CreateResourceCommand, bool>
    {
        public Task<bool> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
