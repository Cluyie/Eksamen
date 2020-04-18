using System;
using Models.Interfaces;

namespace SignalR_Microservice.Models
{
    public class AvailableTime : IAvailableTime
    {
        public Guid Id { get; set; }
        public DateTime From { get; set; }
        public bool Recurring { get; set; }
        public DateTime To { get; set; }
        public Guid ResourceId { get; set; }
    }
}