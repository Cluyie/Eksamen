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
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        public Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
