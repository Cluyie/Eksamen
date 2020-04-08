using System;
using System.Collections.Generic;

namespace Models.Interfaces
{
    public interface IResource<TReservation, TReserveTime, TAvailableTime>
        where TReservation : IReservation<TReserveTime>
        where TReserveTime : IReserveTime
        where TAvailableTime : IAvailableTime
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        List<TReservation> Reservations { get; set; }
        List<TAvailableTime> TimeSlots { get; set; }
    }
}