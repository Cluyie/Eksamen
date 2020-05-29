using RabbitMQ.Bus.Commands;
using UCLDreamTeam.User.Domain.Models;

namespace UCLDreamTeam.User.Domain.Commands
{
    public class UpdateUserCommand : Command
    {
        public Models.User InputUser { get; set; }
        public Models.User UserToChange { get; set; }
        public Role Role { get; set; }

        public UpdateUserCommand(Models.User inputUser, Models.User userToChange, Role role = null)
        {
            InputUser = inputUser;
            UserToChange = userToChange;
            Role = role ?? new Role { RoleName = "User" };
        }
    }
}
