using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;

namespace Data_Access_Layer.Interfaces
{
    public interface IReservation
    {
        Guid Id { get; set; }
        List<ReserveTime> Timeslot { get; set; }
        Guid UserId { get; set; }
    }
}