using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace SignalR_Microservice.Hubs
{
    public class QueueHub : Hub
    {
        private readonly IQueueService _queueService;

        public QueueHub(IQueueService queueService)
        {
            _queueService = queueService;
        }

        public void Enqueue(Guid ticketId)
        {
            _queueService.Enqueue(Context.ConnectionId, ticketId);
        }

        public async Task GetIndex(string id, int index)
        {
            await Clients.Client(id).SendAsync("ReceiveIndex", index);
        }

        public async Task GetQueueCount()
        {
            await Clients.Caller.SendAsync("ReceiveQueueCount", _queueService.QueueCount);
        }

        public async Task GetNextCustomer(string groupId)
        {
            var taskId = _queueService.Dequeue(groupId);
            await Clients.Caller.SendAsync("ReceiveTaskId", taskId);
        }

        public async Task SendGroupId(string id, string groupId)
        {
            await Clients.Client(id).SendAsync("ReceiveGroupId", groupId);
        }
    }
}