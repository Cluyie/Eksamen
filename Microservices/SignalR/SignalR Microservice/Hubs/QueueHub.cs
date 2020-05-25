using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using SignalR_Microservice.Services;

namespace SignalR_Microservice.Hubs
{
    public class QueueHub : Hub
    {
        private readonly IQueueService _queueService;
        private QueueSendService SendService { get; set; }

        public QueueHub(IQueueService queueService, QueueSendService sendService)
        {
            _queueService = queueService;
            SendService = sendService;
        }

        public void Enqueue(string ticketId)
        {
            var test = Guid.Parse(ticketId);
            _queueService.Enqueue(Context.ConnectionId, test);
        }

        public async Task GetIndex(int index)
        {
            await SendService.SendToReceiveIndex(Context.ConnectionId, index);
        }

        public async Task GetQueueCount()
        {
            await SendService.SendReceiveQueueCount(Context.ConnectionId, _queueService.QueueCount);
            //await Clients.Caller.SendAsync("ReceiveQueueCount");
        }

        public async Task GetNextCustomer(string groupId)
        {
            var taskId = _queueService.Dequeue(groupId);
            await SendService.SendReceiveTaskId(Context.ConnectionId, await taskId);
            //await Clients.Caller.SendAsync("ReceiveTaskId", taskId);
        }

        //public async Task SendGroupId(string id, string groupId)
        //{
        //    SendService.SendReceiveGroupId(Context.ConnectionId, );
        //    //await Clients.Client(id).SendAsync("ReceiveGroupId", groupId);
        //}
    }
}