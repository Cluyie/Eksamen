using RabbitMQ.Bus.Commands;

namespace UCLDreamTeam.Resource.Domain.InternalCommands
{
    public class UpdateResourceCommand : Command
    {

        public Models.Resource InputResource { get; set; }
        public Models.Resource ResourceToChange { get; set; }
        public UpdateResourceCommand(Models.Resource inputResource, Models.Resource resourceToChange)
        {
            InputResource = inputResource;
            ResourceToChange = resourceToChange;
        } 
    }
}
