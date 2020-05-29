using System;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace XamarinFormsApp.Model
{
    public class UserTicket : IUserTicket
    {
        public Guid UserId { get; set; }
        public Guid TicketId { get; set; }
    }
}