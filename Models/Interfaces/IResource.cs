using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IResource
    {
        Guid Id { get; set; }
        string strName { get; set; }
        List<IReservation> Reservations { get; set; }
        List<IAvailableTime> TimeSlot { get; set; }
    }
}
