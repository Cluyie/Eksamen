using RabbitMQ.Bus.Commands;

namespace UCLDreamTeam.User.Domain.Commands
{
    public class UpdateUserCommand : Command
    {
        public Models.User InputUser { get; set; }
        public Models.User UserToChange { get; set; }

        public UpdateUserCommand(Models.User inputUser, Models.User userToChange)
        {
            InputUser = inputUser;
            UserToChange = userToChange;
        }
    }
}
