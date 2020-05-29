using RabbitMQ.Bus.Commands;
using UCLDreamTeam.User.Domain.Models;

namespace UCLDreamTeam.User.Domain.Commands
{
    public class DeleteUserCommand : Command
    {
        public Models.User User { get; set; }

        public DeleteUserCommand(Models.User user)
        {
            User = user;
        }
    }
}
