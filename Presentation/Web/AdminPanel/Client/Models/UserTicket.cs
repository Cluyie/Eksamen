using System;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace AdminPanel.Client.Models
{
    public class UserTicket : IUserTicket
    {
        public Guid UserId { get; set; }
        public Guid TicketId { get; set; }
    }
}