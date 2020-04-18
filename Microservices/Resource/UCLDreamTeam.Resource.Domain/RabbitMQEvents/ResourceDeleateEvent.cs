using RabbitMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace UCLDreamTeam.Resource.Domain.RabbitMQEvents
{
    public class ResourceDeleteEvent:Event
    {
        public Models.Resource Resource { get; set; }

        public ResourceDeleteEvent(Models.Resource resource)
        {
            Resource = resource;
        }
    }
}
