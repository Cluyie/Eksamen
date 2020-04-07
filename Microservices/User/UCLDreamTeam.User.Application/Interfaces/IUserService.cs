using System;
using System.Threading.Tasks;
using UCLDreamTeam.User.Domain.Models;

namespace UCLDreamTeam.User.Application.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<string>> RegisterAsync(Domain.Models.User user);

        Task<ApiResponse<Domain.Models.User>> Update(Domain.Models.User user);

        // ----- Internal methods -----

        Task<Domain.Models.User> GetUserFromIdAsync(Guid id);

        Task<Domain.Models.User> GetUserFromUserNameAsync(string userName);
    }
}