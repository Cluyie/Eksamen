using System;
using RabbitMQ.Bus.Events;
using SignalR_Microservice.Models;

namespace SignalR_Microservice.Events
{
    public class ReservationCreatedEvent : Event
    {
        public ReservationCreatedEvent(Guid id, Guid userId, Guid resourceId, ReserveTime timeslot)
        {
            Id = id;
            UserId = userId;
            ResourceId = resourceId;
            Timeslot = timeslot;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ResourceId { get; set; }
        public ReserveTime Timeslot { get; set; }
    }
}