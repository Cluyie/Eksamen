using RabbitMQ.Bus.Commands;
using UCLDreamTeam.UserServiceApi.Domain.Models;

namespace UCLDreamTeam.UserServiceApi.Domain.Commands
{
    public class NoUserFoundCommand : Command
    {
        private readonly User _userToChange;

        public NoUserFoundCommand(User userToChange)
        {
            _userToChange = userToChange;
        }
    }
}