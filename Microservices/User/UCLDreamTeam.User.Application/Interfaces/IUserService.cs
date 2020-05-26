using System;
using System.Threading.Tasks;

namespace UCLDreamTeam.User.Application.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(Domain.Models.User user);

        Task<Domain.Models.User> Update(Domain.Models.User user);

        Task<Domain.Models.User> GetUserFromIdAsync(Guid id);

        Task<Domain.Models.User> GetUserFromUserNameAsync(string userName);

        Task DeleteUserFromIdAsync(Guid id);
    }
}