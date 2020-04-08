using System;

namespace Models.Interfaces
{
    public interface IReservation<TReserveTime>
        where TReserveTime : IReserveTime
    {
        Guid Id { get; set; }
        TReserveTime Timeslot { get; set; }
        Guid UserId { get; set; }
        Guid ResourceId { get; set; }
    }
}