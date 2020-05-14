using System;
using System.Collections.Generic;
using UCLDreamTeam.Resource.Domain.Interfaces;

namespace UCLDreamTeam.SharedInterfaces.Interfaces
{
    public interface IResource<TReservation, TReserveTime, TAvailableTime>
        where TReservation : IReservation<TReserveTime>
        where TReserveTime : IReserveTime
        where TAvailableTime : IAvailableTime
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TAvailableTime> TimeSlots { get; set; }
        List<TReservation> Reservations { get; set; }
    }
}