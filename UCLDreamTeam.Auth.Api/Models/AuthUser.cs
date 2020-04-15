using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UCLDreamTeam.Auth.Api.Models
{
    public class AuthUser
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }
    }
}
