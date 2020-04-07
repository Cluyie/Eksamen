using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Bus.Commands;
using UCLDreamTeam.UserServiceApi.Domain.Models;

namespace UCLDreamTeam.UserServiceApi.Domain.Commands
{
    public class UpdateUserCommand : Command
    {
        public User User { get; set; }

        public UpdateUserCommand(User user)
        {
            User = user;
        }
    }
}
