using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceInterface
{
    interface IAvaiableTime
    {
        Guid Id { get; set; }
        bool Recurring { get; set; }
        DateTime From { get; set; }
        DateTime To { get; set; }
        Guid ResourceId { get; set; }
    }
}
