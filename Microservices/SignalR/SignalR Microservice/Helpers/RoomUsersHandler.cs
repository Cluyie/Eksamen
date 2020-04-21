using SignalR_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SignalR_Microservice.Helpers
{
    public class RoomUsersHandler
    {
        public Dictionary<string,List<User>> AddUserToRoom(Dictionary<string,List<User>> roomsWithUsers, string roomName, User currentUser)
        {
            if (!roomsWithUsers.ContainsKey(roomName))
            {
                roomsWithUsers.Add(roomName, new List<User>());               
            }

            if (roomsWithUsers[roomName].Any(user => user.Id == currentUser.Id))
            {
                throw new ArgumentException($"The user with id {currentUser.Id} already exists in room '{roomName}'");
            }
            else
            {
                bool space = CheckIfAvailableSpace(roomsWithUsers, roomName);

                if (space == true)
                {
                    roomsWithUsers[roomName].Add(currentUser);
                }
            }
            return roomsWithUsers;
        }

        public bool CheckIfAvailableSpace(Dictionary<string,List<User>> roomsWithUsers, string roomName)
        {
            bool space = false;

            try
            {
                if (roomsWithUsers[roomName].Count < 2)
                {
                    space = true;
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("room name doesn't exist", e);
            }
            return space;
        }
    }
}
