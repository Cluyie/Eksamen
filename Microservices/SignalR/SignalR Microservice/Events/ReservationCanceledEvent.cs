using System;
using RabbitMQ.Bus.Events;

namespace SignalR_Microservice.Events
{
    public class ReservationCanceledEvent : Event
    {
        public ReservationCanceledEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}