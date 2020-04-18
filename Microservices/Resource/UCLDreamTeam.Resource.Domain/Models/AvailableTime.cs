using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Interfaces;

namespace UCLDreamTeam.Resource.Domain.Models
{
    public class AvailableTime : IAvailableTime
    {
        public Guid Id { get ; set ; }

        [ForeignKey("Resource")]
        public Guid ResourceId { get ; set ; }
        public bool Recurring { get ; set ; }
        public DateTime From { get ; set ; }
        public DateTime To { get ; set ; }
    }
}
