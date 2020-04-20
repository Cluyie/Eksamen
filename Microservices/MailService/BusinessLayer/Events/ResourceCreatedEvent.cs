using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Interfaces;
using RabbitMQ.Bus.Events;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Mail.Domain.Events
{
    public class ResourceCreatedEvent : Event
    {
        public IResource<IReservation<IReserveTime>, IReserveTime, IAvailableTime> Resource { get; }

        public ResourceCreatedEvent(IResource<IReservation<IReserveTime>, IReserveTime, IAvailableTime> resource)
        {
            Resource = resource;
        }
    }
}
