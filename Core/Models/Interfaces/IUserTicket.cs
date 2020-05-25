using System;

namespace UCLDreamTeam.SharedInterfaces.Interfaces
{
    public interface IUserTicket
    {
        Guid UserId { get; set; }
        Guid TicketId { get; set; }
    }
}