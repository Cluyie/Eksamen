using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Resource : IResource
    {
        public Guid Id { get; set; }
        public string strName { get; set; }
        public List<IReservation> Reservations { get; set; }
        public List<IAvailableTime> TimeSlot { get; set; }
    }
}
