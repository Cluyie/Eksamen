﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalR_Microservice.Models;

namespace SignalR_Microservice.Hubs
{
    public class ResourceHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            System.Console.WriteLine(Context.ConnectionId);
            return base.OnConnectedAsync();
        }
        public async Task CreateResource(Resource resource)
        {
            await Clients.All.SendAsync("CreateResource", resource);
        }

        public async Task UpdateResource(Resource resource)
        {
            await Clients.All.SendAsync("UpdateResource", resource);
        }

        public async Task DeleteResource(Resource resource)
        {
            await Clients.All.SendAsync("DeleteResource", resource);
        }
    }
}