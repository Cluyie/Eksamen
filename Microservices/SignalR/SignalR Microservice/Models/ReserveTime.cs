using System;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace SignalR_Microservice.Models
{
    public class ReserveTime : IReserveTime
    {
        public Guid ReservationId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}