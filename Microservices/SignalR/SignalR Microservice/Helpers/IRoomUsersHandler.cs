using SignalR_Microservice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR_Microservice.Helpers
{
    public interface IRoomUsersHandler
    {
        Task <(Dictionary<string, List<User>>, bool, User )> AddUserToRoom(Dictionary<string, List<User>> roomsWithUsers, string roomName, User currentUser);
        bool CheckIfAvailableSpace(Dictionary<string, List<User>> roomsWithUsers, string roomName);
    }
}