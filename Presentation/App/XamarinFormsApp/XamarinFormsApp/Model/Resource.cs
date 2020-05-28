using System;
using System.Collections.Generic;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace XamarinFormsApp.Model
{
    public class Resource : AutoMapper.Profile, IResource<Reservation, ReserveTime, AvailableTime>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<AvailableTime> TimeSlots { get; set; }
    }
}