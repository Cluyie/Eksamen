using System;

namespace Data_Access_Layer.Interfaces
{
    interface IAvailableTime
    {
        bool Available { get; set; }
        DateTime From { get; set; }
        int? Recurring { get; set; }
        DateTime To { get; set; }
    }
}