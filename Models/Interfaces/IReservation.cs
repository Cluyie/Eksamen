using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IReservation
    {
        Guid Id { get; set; }
        List<IReserveTime> Timeslot { get; set; }
        Guid UserId { get; set; }
    }
}
