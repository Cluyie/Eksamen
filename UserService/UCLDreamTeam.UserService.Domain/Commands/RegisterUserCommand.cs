using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using RabbitMQ.Bus.Commands;
using UCLDreamTeam.UserServiceApi.Domain.Models;

namespace UCLDreamTeam.UserServiceApi.Domain.Commands
{
    public class RegisterUserCommand : Command
    {
        private readonly User _user;

        public RegisterUserCommand(User user)
        {
            _user = user;
        }
    }
}
