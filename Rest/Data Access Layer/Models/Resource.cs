using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer.Models
{
    public class Resource : IResource
    {
        public Guid Id { get; set; }
        public string strName { get; set; }
        public List<AvailableTime> TimeSlot { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
