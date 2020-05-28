using System;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Ticket.Domain.EventHandlers
{
    public class Role : IRole
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}