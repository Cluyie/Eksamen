using Models;
using System;
using System.Collections.Generic;

namespace Models
{
    public interface IReservation
    {
        Guid Id { get; set; }
        List<IReserveTime> Timeslot { get; set; }
        Guid UserId { get; set; }
    }
}