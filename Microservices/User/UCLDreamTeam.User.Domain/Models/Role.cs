using System;
using System.Collections.Generic;
using System.Text;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.User.Domain.Models
{
    public class Role : IRole
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
