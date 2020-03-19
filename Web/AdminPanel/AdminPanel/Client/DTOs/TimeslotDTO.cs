using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Client.DTOs
{
    public class TimeslotDTO
    {
        public Guid Id;

        public bool Available;

        public int? Recurring;

        public DateTime FromDateTime;

        public DateTime ToDateTime;
    }
}