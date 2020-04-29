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
        IHubContext<QueueHub> hub
        public QueSendService(IHubContext<QueueHub> hubContext)
        {

        }
        public void SendToReceiveIndex(int index)
        {

        }
    }
}
