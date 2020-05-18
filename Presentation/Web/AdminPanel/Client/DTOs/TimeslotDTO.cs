using System;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace AdminPanel.Client.DTOs
{
    public class TimeslotDTO : IAvailableTime
    {
        public Guid Id { get; set; }
        public Guid ResourceId { get; set; }
        public bool Recurring { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}