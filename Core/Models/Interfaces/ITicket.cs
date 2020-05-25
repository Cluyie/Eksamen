using System;
using System.Collections.Generic;

namespace UCLDreamTeam.SharedInterfaces.Interfaces
{
    public interface ITicket<TMessage, TUserTicket> 
        where TMessage : IMessage 
        where TUserTicket : IUserTicket
    {
        public Guid Id { get; set; }
        public bool? Active { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public List<TMessage> Messages { get; set; }
        List<TUserTicket> UserTickets { get; set; }
        public Guid? ReservationId { get; set; }
    }
}