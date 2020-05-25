using System;

namespace AdminPanel.Client.DTOs
{
    public class TimeslotDTO
    {
        public Guid Id { get; set; }

        public bool Available { get; set; }

        public bool Recurring { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}