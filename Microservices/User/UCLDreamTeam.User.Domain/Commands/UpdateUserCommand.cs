using RabbitMQ.Bus.Commands;

namespace UCLDreamTeam.User.Domain.Commands
{
    public class UpdateUserCommand : Command
    {
        public Models.User User { get; set; }

        public UpdateUserCommand(Models.User user)
        {
            User = user;
        }
    }
}
