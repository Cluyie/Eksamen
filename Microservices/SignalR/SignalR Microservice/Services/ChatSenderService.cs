using Microsoft.AspNetCore.SignalR;
using SignalR_Microservice.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Microservice.Services
{
    public class ChatSenderService
    {

        IHubContext<ChatHub> ClientHub;
        public ChatSenderService(IHubContext<ChatHub> clientHub)
        {
            ClientHub = clientHub;
        }
        public async Task SendSendMessageToGroup(string message, string roomName)
        {
            ClientHub.Clients.Group(roomName).SendAsync("SendMessageToGroup", message);
        }
    }
}
