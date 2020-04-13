using ResourceInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceMicrosDtabase.Models
{
    public class Resource<TAvaiablTime> : IResource<TAvaiablTime> where TAvaiablTime : IAvaiableTime
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TAvaiablTime> TimeSlots { get; set; }
    }
}
