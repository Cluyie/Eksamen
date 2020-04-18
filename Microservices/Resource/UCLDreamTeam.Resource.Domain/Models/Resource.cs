using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Models.Interfaces;

namespace UCLDreamTeam.Resource.Domain.Models
{
    public class Resource : IResource<Reservation, ReserveTime, AvailableTime>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public List<Reservation> Reservations { get; set; }
        public List<AvailableTime> TimeSlots { get; set; }
    }
}
