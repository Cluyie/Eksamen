using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Reservation.Domain.Models
{
    public class ReserveTime : IReserveTime
    {
        [Key] [ForeignKey("Reservation")] public Guid ReservationId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}