using System;
using System.Collections.Generic;

namespace Models
{
    public interface IResource
    {
        Guid Id { get; set; }
        string strName { get; set; }
        List<IReservation> Reservations { get; set; }
        List<IAvailableTime> TimeSlot { get; set; }
    }
}