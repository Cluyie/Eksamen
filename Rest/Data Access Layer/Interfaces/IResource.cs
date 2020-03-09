using System;
using System.Collections.Generic;

namespace Data_Access_Layer.Interfaces
{
    interface IResource
    {
        Guid Id { get; set; }
        List<Reservation> Reservations { get; set; }
        string strName { get; set; }
        List<AvailableTime> Timeslot { get; set; }
    }
}