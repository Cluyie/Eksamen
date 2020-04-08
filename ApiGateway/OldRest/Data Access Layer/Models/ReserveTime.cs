using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Interfaces;

namespace Data_Access_Layer.Models
{
    public class ReserveTime : IReserveTime
    {
        [Key] [ForeignKey("Reservation")] public Guid ReservationId { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}