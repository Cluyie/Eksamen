using System;
using RabbitMQ.Bus.Events;
using UCLDreamTeam.Mail.Domain.Models;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Mail.Domain.Events
{
    public class ResourceUpdatedEvent : Event
    {
        public Resource Resource { get; }

        public ResourceUpdatedEvent(Resource resource)
        {
            Resource = resource;
        }   
    }
}
