using System;
using System.Collections.Generic;
using System.Text;

namespace ResourcInterface
{
    public interface IAvaiableTime
    {
        Guid Id { get; set; }
        Guid ResourceId { get; set; }
        /// <summary>
        ///  This thels the start date and time of when the resource is avaiable
        /// </summary>
        DateTime From { get; set; }
        /// <summary>
        /// This variable tels when it ends
        /// if the itme is Recurring tells also when it stops
        /// </summary>
        DateTime To { get; set; }
        /// <summary>
        /// this object tels when this object reacore
        /// </summary>
        bool Recurring { get; set; }

    }
}
