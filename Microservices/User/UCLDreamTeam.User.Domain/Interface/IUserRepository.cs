using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace UCLDreamTeam.User.Domain.Interface
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUserAsync(Models.User user);
        Task<Models.User> GetUserAsync(Guid id);
        Task<IdentityResult> UpdateUserAsync(Models.User requestInputUser, Models.User dbUser);
        Task<IdentityResult> DeleteUserAsync(Models.User user);
        Task<Models.User> GetFromUserNameAsync(string userName);
    }
}
