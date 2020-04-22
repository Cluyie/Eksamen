using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Bus.Events;
using UCLDreamTeam.Mail.Domain.Models;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Mail.Domain.Events
{
    public class ResourceCreatedEvent : Event
    {
        public Resource Resource { get; }

        public ResourceCreatedEvent(Resource resource)
        {
            Resource = resource;
        }
    }
}
