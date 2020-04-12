using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Interfaces;

namespace UCLDreamTeam.Reservation.Domain.Models
{
    public class Reservation : IReservation<ReserveTime>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("Resource")] public Guid ResourceId { get; set; }
        public ReserveTime Timeslot { get; set; }
    }
}