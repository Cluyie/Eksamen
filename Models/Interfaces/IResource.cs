using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IResource<TReservation, TReserveTime, TAvailableTime> 
        where TReservation : IReservation<TReserveTime>
        where TReserveTime : IReserveTime
        where TAvailableTime : IAvailableTime
    {
        Guid Id { get; set; }
        string Name { get; set; }
        List<TReservation> Reservations { get; set; }
        List<TAvailableTime> TimeSlot { get; set; }
    }
}
