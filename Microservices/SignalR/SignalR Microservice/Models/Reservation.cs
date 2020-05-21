using System;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace SignalR_Microservice.Models
{
    public class Reservation : IReservation<ReserveTime>
    {
        public Guid Id { get; set; }
        public ReserveTime Timeslot { get; set; }
        public Guid UserId { get; set; }
        public Guid ResourceId { get; set; }
    }
}