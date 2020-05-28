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

        public async Task Create(AuthUser user, Role inputRole)
        {
            _authContext.Add(user);
            var role = await _authContext.Roles.FirstOrDefaultAsync(r => r.RoleName.Contains(inputRole.RoleName));
            await _authContext.UserRoles.AddAsync(new UserRole {RoleId = role.RoleId, AuthUserId = user.Id});
            await _authContext.SaveChangesAsync();
        }

        public async Task<AuthUser> GetUserFromUserNameOrEmailAsync(string userNameOrEmail)
        {
            AuthUser authUser = await _authContext.AuthUsers
              .Include(u => u.UserRoles)
              .ThenInclude(u => u.Role)
              .FirstOrDefaultAsync(u => u.UserName.Contains(userNameOrEmail) || u.Email.Contains(userNameOrEmail));
            return authUser;
        }

        public async Task UpdateUser(AuthUser user, Role role)
        {
            _authContext.AuthUsers.Update(user);
            await _authContext.SaveChangesAsync();
        }

        public async Task<AuthUser> GetUserFromId(Guid id)
        {
            return await _authContext.AuthUsers.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task Delete(AuthUser user)
        {
            _authContext.Remove(user);
            await _authContext.SaveChangesAsync();
        }
    }
}
