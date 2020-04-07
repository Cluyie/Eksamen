using RabbitMQ.Bus.Commands;

namespace UCLDreamTeam.User.Domain.Commands
{
    public class NoUserFoundCommand : Command
    {
        public Models.User User { get; set; }

        public NoUserFoundCommand(Models.User user)
        {
            User = user;
        }
    }
}