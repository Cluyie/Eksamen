using MediatR;
using Resource.Domain.InternealCommands;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Domain.InternealCommands
{
    public class CreatResourceCommandsHandler : IRequestHandler<CreatResourceCommand, bool>
    {
        public Task<bool> Handle(CreatResourceCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
