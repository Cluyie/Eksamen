using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Reservation : IReservation
    {
        public Guid Id { get; set; }
        public List<IReserveTime> Timeslot { get; set; }
        public Guid UserId { get; set; }
    }
}
