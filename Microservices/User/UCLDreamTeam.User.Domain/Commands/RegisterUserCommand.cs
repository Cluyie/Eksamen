using RabbitMQ.Bus.Commands;
using UCLDreamTeam.User.Domain.Models;

namespace UCLDreamTeam.User.Domain.Commands
{
    public class RegisterUserCommand : Command
    {
        public Models.User User { get; set; }
        public Role Role { get; set; }

        public RegisterUserCommand(Models.User user, Role role = null)
        {
            User = user;
            Role = role ?? new Role { RoleName = "User" };
        }
    }
}
