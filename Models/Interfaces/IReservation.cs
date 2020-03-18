using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IReservation<TReserveTime>
        where TReserveTime : IReserveTime
    {
        Guid Id { get; set; }
        List<TReserveTime> Timeslot { get; set; }
        Guid UserId { get; set; }
    }
}
