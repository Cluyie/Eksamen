using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Bus.Bus.Interfaces;
using SignalR.Domain.Events;

namespace SignalR.Domain.EventHandlers
{
    public class ReservationCreatedEventHandler : IEventHandler<ReservationCreatedEvent>
    {
        public Task Handle(ReservationCreatedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}