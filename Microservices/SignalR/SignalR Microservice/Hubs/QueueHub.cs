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
        //private readonly IQueueService _queueService;
        private readonly IServiceProvider _services;


        public QueueHub(/*IQueueService queueService, */IServiceProvider services)
        {
            //_queueService = queueService;
            _services = services;
        }

        public void Enqueue()
        {
            var _queueService = _services.GetRequiredService<IQueueService>();
            _queueService.Enqueue(Context.ConnectionId);
            //_queueService.Enqueue(Context.ConnectionId);
        }

        public async Task GetIndex(string id, int index)
        {
            await Clients.Client(id).SendAsync("ReceiveIndex", index);
        }

        public void GetNextCustomer(string groupId)
        {
            var _queueService = _services.GetRequiredService<IQueueService>();
            _queueService.Dequeue(groupId);
            //_queueService.Dequeue(groupId);
        }

        public async Task SendGroupId(string id, string groupId)
        {
            await Clients.Client(id).SendAsync("ReceiveGroupId", groupId);
        }
    }
}