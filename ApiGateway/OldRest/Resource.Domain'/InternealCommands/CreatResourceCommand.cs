using RabbitMQ.Bus.Commands;
using Resource.Domain.Models;

namespace Resource.Domain.InternealCommands
{
    public class CreatResourceCommand : Command
    {
        public Resource<AvaiableTime> Resource { get; set; }
        public CreatResourceCommand(Resource<AvaiableTime> resource)
        {
            Resource = resource;
        }
    }
}
