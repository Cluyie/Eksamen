using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Bus.Bus.Interfaces;
using SignalR.Domain.Events;

namespace SignalR.Domain.EventHandlers
{
    public class ReservationCanceledEventHandler : IEventHandler<ReservationCanceledEvent>
    {
        public Task Handle(ReservationCanceledEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
