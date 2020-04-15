using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCLDreamTeam.Auth.Api.Models;

namespace UCLDreamTeam.Auth.Api.Infrastructure
{
    public class AuthRepository
    {
        public async Task<AuthUser> GetUserFromUserNameOrEmailAsync(string userNameOrEmail)
        {
            throw new NotImplementedException();
        }
    }
}
