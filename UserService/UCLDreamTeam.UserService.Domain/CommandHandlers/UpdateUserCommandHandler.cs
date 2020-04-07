using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UCLDreamTeam.UserServiceApi.Domain.Commands;
using UCLDreamTeam.UserServiceApi.Domain.Services;

namespace UCLDreamTeam.UserServiceApi.Domain.CommandHandlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        public Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
