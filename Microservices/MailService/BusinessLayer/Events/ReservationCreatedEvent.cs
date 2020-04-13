using System;
using RabbitMQ.Bus.Events;
using UCLDreamTeam.Mail.Domain.Models;

namespace UCLDreamTeam.Mail.Domain.Events
{
    public class ReservationCreatedEvent : Event
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ResourceId { get; set; }
        public ReserveTime Timeslot { get; set; }

        public ReservationCreatedEvent(Guid id, Guid userId, Guid resourceId, ReserveTime timeslot)
        {
            Id = id;
            UserId = userId;
            ResourceId = resourceId;
            Timeslot = timeslot;
        }
    }
}