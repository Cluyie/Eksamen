using System;
using System.Collections.Generic;
using System.Text;

namespace UCLDreamTeam.SharedInterfaces.Interfaces
{
    public interface IConection
    {
        Guid GroopId { get; set; }
        Guid ResourceId { get; set; }
        Guid KundeId { get; set; }
        Guid SuportId { get; set; }

    }
}
