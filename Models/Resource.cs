using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Resource
    {
        public Guid Id { get; set; }
        public string strName { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<AvailableTime> TimeSlot { get; set; }
    }
}
