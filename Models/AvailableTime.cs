using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AvailableTime
    {
        public bool Available { get; set; }
        public DateTime From { get; set; }
        public int? Recurring { get; set; }
        public DateTime To { get; set; }
    }
}
