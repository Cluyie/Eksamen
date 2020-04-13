using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceInterface
{
    interface IResource<TAvaiablTime> where TAvaiablTime:IAvaiableTime
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        List<TAvaiablTime> TimeSlots { get; set; }
    }
}
