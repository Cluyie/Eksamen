using System.Threading.Tasks;
using AdminPanel.Client.DTOs;
using AdminPanel.Client.Models;

namespace AdminPanel.Client.Services
{
    public interface IAuthService
    {
        public Task<User> GetCurrentUser();

        Task<bool> Login(LoginDTO loginDTO);

        void Logout();
    }
}