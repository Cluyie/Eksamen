using System;
using System.Collections.Generic;

namespace ResourcInterface
{
    public interface IResource<TAvaiabletime> where TAvaiabletime : IAvaiableTime
    {
        Guid Id { get; set; }
        /// <summary>
        /// The name of the resource 
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// This object holds a description of the rescource
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// This is a list of Times the resource is avaiable
        /// They can be reacoring more about the in Avaiabletime
        /// </summary>
        List<TAvaiabletime> Avaiabletimes { get; set; }

    }
}
