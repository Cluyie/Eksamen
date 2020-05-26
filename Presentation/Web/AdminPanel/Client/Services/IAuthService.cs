using System.Threading.Tasks;
using AdminPanel.Client.DTOs;
using AdminPanel.Client.Models;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace AdminPanel.Client.Services
{
    public interface IAuthService
    {
        Task<User> GetCurrentUser();

        Task<bool> Login(LoginDTO loginDTO);

        void Logout();
    }
}