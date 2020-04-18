using RabbitMQ.Bus.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UCLDreamTeam.Resource.Domain.InternalCommands
{
    public class DeleteResourceCommands: Command
    {
        public Models.Resource Resource { get; set; }
        public DeleteResourceCommands(Models.Resource resource)
        {
            Resource = resource;
        }
    }
}
