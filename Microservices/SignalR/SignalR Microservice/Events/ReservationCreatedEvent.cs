using System;
using RabbitMQ.Bus.Events;
using SignalR_Microservice.Models;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace SignalR_Microservice.Events
{
    public class ReservationCreatedEvent : Event, IReservation<ReserveTime>
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