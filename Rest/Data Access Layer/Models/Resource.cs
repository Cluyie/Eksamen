using System;
using System.Collections.Generic;
using System.Text;
using Models.Interfaces;

namespace Data_Access_Layer.Models
{
    public class Resource : IResource
    {
        public Guid Id { get; set; }
        public string strName { get; set; }
        public List<IAvailableTime> TimeSlot { get; set; }
        public List<IReservation> Reservations { get; set; }
    }
}
