using System;
using RabbitMQ.Bus.Events;

namespace UCLDreamTeam.Reservation.Domain.Events
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