using RabbitMQ.Bus.Events;
using Resource.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resource.Domain.RwbbitMQEvents
{
    public class ResourceDeleateEvent:Event
    {
        public Resource<AvaiableTime> Resource { get; set; }

        public ResourceDeleateEvent(Resource<AvaiableTime> resource)
        {
            Resource = resource;
        }
    }
}
