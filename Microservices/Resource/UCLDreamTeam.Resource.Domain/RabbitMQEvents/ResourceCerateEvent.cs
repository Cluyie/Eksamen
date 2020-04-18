using RabbitMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace UCLDreamTeam.Resource.Domain.RabbitMQEvents
{
    public class ResourceCreateEvent : Event
    {
        public Models.Resource Resource { get; set; }

        public ResourceCreateEvent(Models.Resource resource)
        {
            Resource = resource;
        }
    }
}
