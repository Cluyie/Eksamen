using System;
using System.Collections.Generic;
using System.Text;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Ticket.Domain.Models.Event
{
    public class Conection : IConection
    {
        public Guid GroopId { get; set; }
        public Guid ResourceId { get; set; }
        public Guid KundeId { get; set; }
        public Guid SuportId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
