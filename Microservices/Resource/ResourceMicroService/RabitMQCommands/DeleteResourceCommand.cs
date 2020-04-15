using RabbitMQ.Bus.Commands;
using ResourceMicrosDtabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceMicroService.RabitMQCommands
{
    public class DeleteResourceCommand: Command
    {
        public Resource<AvaiableTime> Resource { get; set; }
        public DeleteResourceCommand(Resource<AvaiableTime> resource)
        {
            Resource = resource;
        }
    }
}
