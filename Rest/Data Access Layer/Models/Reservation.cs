using System;
using System.Text;
using System.Collections.Generic;
using Data_Access_Layer.Models;
using Models.Interfaces;

namespace Data_Access_Layer.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<ReserveTime> Timeslot { get; set; }
    }
}
