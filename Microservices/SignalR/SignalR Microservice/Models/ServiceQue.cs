using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Microservice.Models
{
    public class ServiceQue
    {
        public string SignelRId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ObjctId { get; set; }
        public Guid guid { get; set; }
        public Guid UserId { get; set; }
    }
}
