using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using RabbitMQ.Bus.Commands;
using UCLDreamTeam.UserServiceApi.Domain.Models;

namespace UCLDreamTeam.UserServiceApi.Domain.Commands
{
    public class RegisterUserRejectedCommand : Command
    {
        private readonly User _user;

        public RegisterUserRejectedCommand(User user)
        {
            _user = user;
        }
    }
}
