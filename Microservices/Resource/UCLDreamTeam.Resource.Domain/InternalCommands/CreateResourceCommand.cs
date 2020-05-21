using RabbitMQ.Bus.Commands;

namespace UCLDreamTeam.Resource.Domain.InternalCommands
{
    public class CreateResourceCommand : Command
    {
        public Models.Resource Resource { get; set; }
        public CreateResourceCommand(Models.Resource resource)
        {
            Resource = resource;
        }
    }
}
