using RabbitMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace UCLDreamTeam.Resource.Domain.RabbitMQEvents
{
    public class ResourceUpdatedEvent : Event
    {
        public Models.Resource Resource { get; set; }
        public ResourceUpdatedEvent(Models.Resource resource)
        {
            Resource = resource;
        }
    }
}
