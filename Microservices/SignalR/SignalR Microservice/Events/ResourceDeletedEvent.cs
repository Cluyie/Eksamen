using RabbitMQ.Bus.Events;
using SignalR_Microservice.Models;
using System;
using System.Collections.Generic;

namespace SignalR_Microservice.EventHandlers
{
    public class ResourceDeletedEvent : Event
    {
        public ResourceDeletedEvent(Resource resource)
        {
            Resource = resource;
        }

        public Resource Resource { get; set; }
    }
}