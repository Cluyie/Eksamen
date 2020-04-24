using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace SignalR_Microservice.Models
{
    public class Message :IMessage
    {
        public string Content { get; set; }
        public string Username { get; set; }
        public Guid Id { get; set; }
        public Guid TicketId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Text { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime TimeStamp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Seen { get; set; }
    }
}
