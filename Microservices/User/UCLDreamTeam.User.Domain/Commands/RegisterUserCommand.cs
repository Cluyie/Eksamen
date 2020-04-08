using RabbitMQ.Bus.Commands;

namespace UCLDreamTeam.User.Domain.Commands
{
    public class RegisterUserCommand : Command
    {
        public Models.User User { get; set; }

        public RegisterUserCommand(Models.User user)
        {
            User = user;
        }
    }
}
