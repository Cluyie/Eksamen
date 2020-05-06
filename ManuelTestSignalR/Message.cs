using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace ManuelTestSignalR
{
    public class Message : IMessage
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid GroopId { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}