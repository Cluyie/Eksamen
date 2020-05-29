using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Auth.Api.Models.DTO
{
    public class RoleDTO : IRole
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
