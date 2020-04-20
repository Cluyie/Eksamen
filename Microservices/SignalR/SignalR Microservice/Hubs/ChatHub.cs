using Microsoft.AspNetCore.SignalR;
using SignalR_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Microservice.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message, string groupName)
        {
            await Clients.Group(groupName).SendAsync("SendMessage", $"{message.Username}: {message.Content}");
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("SendMessage", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("SendMessage", $"{Context.ConnectionId} has left the group {groupName}.");
        }
    }
}
