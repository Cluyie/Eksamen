using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCLDreamTeam.UserServiceApi.Domain.Models;

namespace UCLDreamTeam.UserServiceApi.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<string>> RegisterAsync(User user);

        Task<ApiResponse<User>> Update(User user);

        // ----- Internal methods -----

        Task<User> GetUserFromIdAsync(Guid id);

        Task<User> GetUserFromUserNameAsync(string userName);
    }
}