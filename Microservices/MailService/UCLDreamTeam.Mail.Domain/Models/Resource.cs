using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UCLDreamTeam.Mail.Domain.Interfaces;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Mail.Domain.Models
{
    public class Resource : IResource<Reservation, ReserveTime, AvailableTime>, IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public string Description { get; set; }
        [NotMapped]
        public List<Reservation> Reservations { get; set; }
        [NotMapped]
        public List<AvailableTime> TimeSlots { get; set; }
    }
}