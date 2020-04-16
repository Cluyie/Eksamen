using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UCLDreamTeam.Auth.Api.Models;

namespace UCLDreamTeam.Auth.Api.Infrastructure
{
    public class AuthRepository
    {
        private readonly AuthContext _authContext;

        public AuthRepository(AuthContext authContext)
        {
            _authContext = authContext;
        }

        public async Task<AuthUser> GetUserFromUserNameOrEmailAsync(string userNameOrEmail)
        {
            AuthUser authUser = await _authContext.AuthUsers.Include(u => u.UserRoles).ThenInclude(u => u.Role).SingleOrDefaultAsync(u => u.UserName == userNameOrEmail || u.Email == userNameOrEmail);
            return authUser;
        }
    }
}
