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
        private Queue<User> _userQueue;

        public QueueHub(Queue<User> userQueue)
        {
            _userQueue = userQueue;
        }

        public async Task AddToQueueGroup (User currentUser)
        {
            
            await Groups.AddToGroupAsync(currentUser.Id, "queueGroup");
            _userQueue.Enqueue(currentUser);

           
        }

        public async Task<User> RemoveFromQueue()
        {
            if (_userQueue.Count >= 1)
            {
                var UserDequeue = _userQueue.Dequeue();
                await Groups.RemoveFromGroupAsync(UserDequeue.Id, "queueGroup");
                return UserDequeue;
            }
            return null;
        }

    }
}
