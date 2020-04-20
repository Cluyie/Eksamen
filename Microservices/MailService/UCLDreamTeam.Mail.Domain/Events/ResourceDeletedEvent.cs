using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Bus.Events;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Mail.Domain.Events
{
    public class ResourceDeletedEvent : Event
    {
        public Resource Resource { get; }

        public ResourceDeletedEvent(Resource resource)
        {
            Resource = resource;
        }
    }
}
