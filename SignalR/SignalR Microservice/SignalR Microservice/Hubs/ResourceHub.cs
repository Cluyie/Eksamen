using Microsoft.AspNetCore.SignalR;
using Models;
using SignalR_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR_Microservice.Hubs
{
    public class ResourceHub : Hub
    {
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