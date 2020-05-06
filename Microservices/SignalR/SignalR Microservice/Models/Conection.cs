using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace SignalR_Microservice.Models
{
    public class Conection : IConection
    {
        public Guid GroopId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ResourceId { get; set; }
        public Guid KundeId { get; set; }
        public Guid SuportId { get; set; }
    }
}
