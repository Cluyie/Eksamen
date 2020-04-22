using SignalR_Microservice.Models;
using System.Collections.Generic;

namespace SignalR_Microservice.Helpers
{
    public interface IRoomUsersHandler
    {
        Dictionary<string, List<User>> AddUserToRoom(Dictionary<string, List<User>> roomsWithUsers, string roomName, User currentUser);
        bool CheckIfAvailableSpace(Dictionary<string, List<User>> roomsWithUsers, string roomName);
    }
}