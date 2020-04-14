using RabbitMQ.Bus.Commands;

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