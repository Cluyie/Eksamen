using System;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Resource.Domain.Models
{
    public class ReserveTime : IReserveTime
    {
        public Guid ReservationId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}