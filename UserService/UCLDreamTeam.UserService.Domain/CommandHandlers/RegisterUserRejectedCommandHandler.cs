using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UCLDreamTeam.UserServiceApi.Domain.Commands;
using UCLDreamTeam.UserServiceApi.Domain.Models;

namespace UCLDreamTeam.UserServiceApi.Domain.CommandHandlers
{
    public class RegisterUserRejectedCommandHandler : IRequestHandler<RegisterUserRejectedCommand, bool>
    {
        public Task<bool> Handle(RegisterUserRejectedCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
