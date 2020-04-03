using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Client.DTOs
{
    public class TimeslotDTO
    {
        public Guid Id { get; set; }

        public bool Available { get; set; }

        public int? Recurring { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}