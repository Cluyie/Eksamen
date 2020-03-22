using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Models.Interfaces;

namespace Data_Access_Layer.Models
{
    public class ReserveTime : IReserveTime
    {
        public Guid Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        [ForeignKey("Reservation")]
        public Guid ReservationId { get; set; }
    }
}
