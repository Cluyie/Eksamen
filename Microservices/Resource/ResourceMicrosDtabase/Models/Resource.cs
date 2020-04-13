using ResourcInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceMicrosDtabase.Models
{
    public class Resource : IResource<AvaiableTime> where TAvaiabletime : IAvaiableTime
    {
        public Guid Id { get ; set ; }
        public string Name { get ; set ; }
        public string Description { get ; set ; }
        public List<AvaiableTime> Avaiabletimes { get ; set ; }
    }
}
