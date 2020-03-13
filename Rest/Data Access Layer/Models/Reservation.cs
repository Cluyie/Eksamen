using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer.Models
{
    public class Reservation : IReservation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<ReserveTime> Timeslot { get; set; }
    }
}
