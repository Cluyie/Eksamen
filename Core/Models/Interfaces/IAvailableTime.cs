using System;

namespace Models.Interfaces
{
    public interface IAvailableTime
    {
        Guid Id { get; set; }
        bool Available { get; set; }
        DateTime From { get; set; }
        int? Recurring { get; set; }
        DateTime To { get; set; }
        Guid ResourceId { get; set; }
    }
}