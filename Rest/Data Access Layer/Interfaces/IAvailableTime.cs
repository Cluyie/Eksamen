using System;

namespace Data_Access_Layer.Interfaces
{
    public interface IAvailableTime
    {
        bool Available { get; set; }
        DateTime From { get; set; }
        int? Recurring { get; set; }
        DateTime To { get; set; }
    }
}