using System;

namespace SignalR_Microservice.Events
{
    public class ReservationCanceledEvent
    {
        public ReservationCanceledEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}