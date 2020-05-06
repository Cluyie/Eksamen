using System;
using System.Collections.Generic;
using System.Text;

namespace UCLDreamTeam.SharedInterfaces.Interfaces
{
    public interface IConection
    {
        public Guid GroopId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ResourceId { get; set; }
        public Guid KundeId { get; set; }
        public Guid SuportId { get; set; }

    }
}
