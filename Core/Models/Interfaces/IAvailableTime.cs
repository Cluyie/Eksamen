using System;

namespace UCLDreamTeam.SharedInterfaces.Interfaces
{
    public interface IAvailableTime
    {
        Guid Id { get; set; }
        bool Recurring { get; set; }
        DateTime From { get; set; }
        DateTime To { get; set; }
        Guid ResourceId { get; set; }
    }
}