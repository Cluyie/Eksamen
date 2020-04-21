using System;
using System.ComponentModel.DataAnnotations.Schema;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace Data_Access_Layer.Models
{
    public class Reservation : IReservation<ReserveTime>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey("Resource")] public Guid ResourceId { get; set; }

        public ReserveTime Timeslot { get; set; }
    }
}