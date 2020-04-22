using RabbitMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Text;
using UCLDreamTeam.Reservation.Domain.Models;

namespace UCLDreamTeam.Reservation.Domain.Events
{
    public class ReservationUpdatedEvent : Event
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ResourceId { get; set; }
        public ReserveTime Timeslot { get; set; }

        public ReservationUpdatedEvent(Guid id, Guid userId, Guid resourceId, ReserveTime timeslot)
        {
            Id = id;
            UserId = userId;
            ResourceId = resourceId;
            Timeslot = timeslot;
        }
    }
}
