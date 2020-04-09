using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ResourcInterface;

namespace ResourceMicrosDtabase.Models
{
    public class AvaiableTime : IAvaiableTime
    {
        public Guid Id { get ; set ; }
        [ForeignKey("Resource")]
        public Guid ResourceId { get ; set ; }
        public bool Recurring { get ; set ; }
        public DateTime From { get ; set ; }
        public DateTime To { get ; set ; }
    }
}
