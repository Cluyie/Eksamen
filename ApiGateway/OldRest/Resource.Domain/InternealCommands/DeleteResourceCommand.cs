using RabbitMQ.Bus.Commands;
using Resource.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Domain.InternealCommands
{
    public class DeleteResourceCommands: Command
    {
        public Resource<AvaiableTime> Resource { get; set; }
        public DeleteResourceCommands(Resource<AvaiableTime> resource)
        {
            Resource = resource;
        }
    }
}
