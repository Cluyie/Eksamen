using System;
using RabbitMQ.Bus.Events;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Mail.Domain.Events
{
    public class ResourceUpdatedEvent : Event
    {
        public IResource<IReservation<IReserveTime>, IReserveTime, IAvailableTime> Resource { get; }

        public ResourceUpdatedEvent(IResource<IReservation<IReserveTime>, IReserveTime, IAvailableTime> resource)
        {
            Resource = resource;
        }   
    }
}
