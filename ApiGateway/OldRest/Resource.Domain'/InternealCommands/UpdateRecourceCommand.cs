using RabbitMQ.Bus.Commands;
using Resource.Domain.Models;
namespace Resource.Domain.InternealCommands
{
    public class UpdateRecourceCommand : Command
    {

        public Resource<AvaiableTime> InputResource { get; set; }
        public Resource<AvaiableTime> ResourceToChange { get; set; }
        public UpdateRecourceCommand(Resource<AvaiableTime> inputResource, Resource<AvaiableTime> resourceToChange)
        {
            InputResource = inputResource;
            ResourceToChange = resourceToChange;
        } 
    }
}
