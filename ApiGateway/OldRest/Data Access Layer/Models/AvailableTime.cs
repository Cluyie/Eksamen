using System;
using System.ComponentModel.DataAnnotations.Schema;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace Data_Access_Layer.Models
{
    public class AvailableTime : IAvailableTime
    {
        public Guid Id { get; set; }
        public bool Available { get; set; }
        public bool Recurring { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        [ForeignKey("Resource")] public Guid ResourceId { get; set; }
    }
}