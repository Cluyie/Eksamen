using RabbitMQ.Bus.Commands;
using ResourceMicrosDtabase.Models;
namespace ResourceMicroService.RabitMQCommands
{
    public class RabbitMQUpdateRecource : Command
    {

        public Resource<AvaiableTime> InputResource { get; set; }
        public Resource<AvaiableTime> ResourceToChange { get; set; }
        public RabbitMQUpdateRecource(Resource<AvaiableTime> inputResource, Resource<AvaiableTime> resourceToChange)
        {
            InputResource = inputResource;
            ResourceToChange = resourceToChange;
        } 
    }
}
