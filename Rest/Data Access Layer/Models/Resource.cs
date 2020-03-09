using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer.Models
{
    class Resource : IResource
    {
        public Guid Id { get; set; }
        public string strName { get; set; }
        public List<AvailableTime> Timeslot { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
