using RabbitMQ.Bus.Events;
using SignalR_Microservice.Models;
using System;
using System.Collections.Generic;

namespace SignalR_Microservice.EventHandlers
{
    public class ResourceUpdatedEvent : Event
    {
        public ResourceUpdatedEvent(Resource resource)
        {
            Resource = resource;
        }

        public Resource Resource { get; set; }
    }
}