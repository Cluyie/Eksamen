using SignalR_Microservice.Hubs;
using SignalR_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace SignalR_Microservice.Helpers
{
    public class RoomUsersHandler : IRoomUsersHandler
    {
        private QueueHub queueHub { get; set; }
        
        
        public RoomUsersHandler(QueueHub queueHub)
        {
            this.queueHub = queueHub;
        }

        public async Task< ( Dictionary<string, List<User>> , bool, User)> AddUserToRoom(Dictionary<string, List<User>> roomsWithUsers, string roomName, User currentUser)
        {
            User userToDequeue = new User();
        bool Availablespace = false;

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

                //this only works for the dictionary. This logic should invoke the join method also.
                if (space == true)
                {
                    roomsWithUsers[roomName].Add(currentUser);
                    Availablespace = true;
                    userToDequeue = await queueHub.RemoveFromQueue();
                }
                else //send denne user til Userqueue
                {
                   await queueHub.AddToQueueGroup(currentUser);
                }
            }
            
            return (roomsWithUsers, Availablespace, userToDequeue);
        }

        public bool CheckIfAvailableSpace(Dictionary<string, List<User>> roomsWithUsers, string roomName)
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
