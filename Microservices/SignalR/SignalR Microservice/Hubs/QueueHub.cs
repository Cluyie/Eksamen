using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalR_Microservice.Hubs
{
    public class QueueHub : Hub
    {
        private readonly IQueueService _queueService;

        public QueueHub(IQueueService queueService)
        {
            _queueService = queueService;
        }

        public void Enqueue()
        {
            _queueService.Enqueue(Context.ConnectionId);
        }

        public async Task GetIndex(string id, int index)
        {
            await Clients.Client(id).SendAsync("ReceiveIndex", index);
        }

        public void GetNextCustomer(string groupId)
        {
            _queueService.Dequeue(groupId);
        }

        public async Task SendGroupId(string id, string groupId)
        {
            await Clients.Client(id).SendAsync("ReceiveGroupId", groupId);
        }
    }
}