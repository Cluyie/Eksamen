using System;
using System.Collections.Generic;

namespace UCLDreamTeam.SharedInterfaces.Interfaces
{
    public interface ITicket<TMessage> where TMessage : IMessage
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TMessage> Messages { get; set; }
        public Guid? ResourceId { get; set; }
    }
}