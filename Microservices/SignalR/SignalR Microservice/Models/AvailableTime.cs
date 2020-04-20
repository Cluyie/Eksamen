using System;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace SignalR_Microservice.Models
{
    public class AvailableTime : IAvailableTime
    {
        public Guid Id { get; set; }
        public bool Available { get; set; }
        public DateTime From { get; set; }
        public int? Recurring { get; set; }
        public DateTime To { get; set; }
        public Guid ResourceId { get; set; }
    }
}