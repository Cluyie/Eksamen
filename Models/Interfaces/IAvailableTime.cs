using System;

namespace Models
{
    public interface IAvailableTime
    {
        bool Available { get; set; }
        DateTime From { get; set; }
        int? Recurring { get; set; }
        DateTime To { get; set; }
    }
}