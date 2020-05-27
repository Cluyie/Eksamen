using System;
using System.Collections.Generic;
using System.Text;

namespace UCLDreamTeam.SharedInterfaces.Interfaces
{
    public interface IRole
    {
        public Guid RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
