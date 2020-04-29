using Microsoft.AspNetCore.SignalR;
using SignalR_Microservice.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Microservice.Services
{
    public class ChatSemderService
    {

        IHubContext<ChatHub> QueueHub;
        public ChatSemderService(IHubContext<ChatHub> hubContext)
        {
            QueueHub = hubContext;
        }

    }
}
