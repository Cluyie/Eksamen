using Microsoft.AspNetCore.SignalR;
using SignalR_Microservice.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Microservice.Services
{
    public class QueSendService
    {
        IHubContext<QueueHub> QueueHub;
        public QueSendService(IHubContext<QueueHub> hubContext)
        {
            QueueHub = hubContext;
        }
        public async Task SendToReceiveIndex(string ClinentId ,int index)
        {
           await QueueHub.Clients.Client(ClinentId).SendAsync("ReceiveIndex", index);
        }
        public async Task SendReceiveQueueCount(string ClinentId ,int index)
        {
           await QueueHub.Clients.Client(ClinentId).SendAsync("ReceiveQueueCount", index);
        }
        public async Task SendReceiveTaskId(string ClinentId ,Guid groupId)
        {
           await QueueHub.Clients.Client(ClinentId).SendAsync("ReceiveTaskId", groupId);
        }
        public async Task SendReceiveGroupId(string ClinentId ,string groupId)
        {
           await QueueHub.Clients.Client(ClinentId).SendAsync("ReceiveGroupId", groupId);
        }
    }
}
