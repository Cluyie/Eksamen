using System;
using System.Collections.Generic;
using System.Text;
using Models.Interfaces;

namespace Data_Access_Layer.Models
{
    public class Resource : IResource<Reservation, ReserveTime, AvailableTime>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<AvailableTime> TimeSlot { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
