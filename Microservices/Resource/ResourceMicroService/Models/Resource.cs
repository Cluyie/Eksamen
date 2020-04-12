using ResourcInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceMicroService.Models
{
    public class Resource<TAvaiabletime> : IResource<TAvaiabletime> where TAvaiabletime : IAvaiableTime
    {
        public Guid Id { get ; set ; }
        public string Name { get ; set ; }
        public string Description { get ; set ; }
        public List<TAvaiabletime> Avaiabletimes { get ; set ; }
    }
}
