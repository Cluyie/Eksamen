using System;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Auth.Api.Models
{
    public class Role : IRole
    { 
        public Guid RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
