using RabbitMQ.Bus.Commands;

namespace UCLDreamTeam.User.Domain.Commands
{
    public class RegisterUserRejectedCommand : Command
    {
        public Models.User User { get; set; }

        public RegisterUserRejectedCommand(Models.User user)
        {
            User = user;
        }
    }
}
