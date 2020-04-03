using Models;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinFormsApp.Model
{
    public class Resource : AutoMapper.Profile, IResource<Reservation<ReserveTime>, ReserveTime, AvailableTime>
    {
        public Guid Id { get  ; set  ; }
        public string Name { get  ; set  ; }
        public string Description { get; set; }
        public List<Reservation<ReserveTime>> Reservations { get  ; set  ; }
        public List<AvailableTime> TimeSlots { get  ; set  ; }
    }
}
