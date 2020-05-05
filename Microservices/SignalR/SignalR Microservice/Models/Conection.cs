using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace SignalR_Microservice.Models
{
    public class Conection : IConection
    {
        public string GroopId { get; set; }
    }
}
