using Microsoft.AspNetCore.SignalR;
using SignalR_Microservice.Commands;
using SignalR_Microservice.Helpers;
using SignalR_Microservice.Models;
using SignalR_Microservice.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Microservice.Hubs
{
    public class QueueHub : Hub
    {

        private User currentUser = new User();
        private List<User> connectedUsers = new List<User>();

        private IRoomUsersHandler _roomUsersHandler;

        private Queue<User> UserQueue = new Queue<User>();
        
        public async Task AddToQueueGroup (User currentUser)
        {
            
            await Groups.AddToGroupAsync(currentUser.Id, "queueGroup");
            UserQueue.Enqueue(currentUser);

           
        }

        public async Task<User> RemoveFromQueue()
        {
            var UserDequeue = UserQueue.Dequeue();
            await Groups.RemoveFromGroupAsync(UserDequeue.Id, "queueGroup");
            return UserDequeue;
        }

    }
}
