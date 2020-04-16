using RabbitMQ.Bus.Commands;
using ResourceMicrosDtabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceMicroService.RabitMQCommands
{
    public class CreatResourceCommand: Command
    {
        public Resource<AvaiableTime> Resource { get; set; }
        public CreatResourceCommand(Resource<AvaiableTime> resource)
        {
            Resource = resource;
        }
    }
}
