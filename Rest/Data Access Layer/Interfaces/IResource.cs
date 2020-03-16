using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;

namespace Data_Access_Layer.Interfaces
{
    public interface IResource
    {
        Guid Id { get; set; }
        string strName { get; set; }
        List<Reservation> Reservations { get; set; }
        List<AvailableTime> TimeSlot { get; set; }
    }
}