using RabbitMQ.Bus.Events;
using Resource.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resource.Domain.RwbbitMQEvents
{
    public class ResourceCerateEvent : Event
    {
        public Resource<AvaiableTime> Resource { get; set; }

        public ResourceCerateEvent(Resource<AvaiableTime> resource)
        {
            Resource = resource;
        }
    }
}
