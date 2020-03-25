using Microsoft.AspNetCore.SignalR;
using SignalR_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR_Microservice.Hubs
{
    public class AvailableTimeHub : Hub
    {
        public async Task CreateAvailableTime(AvailableTime availableTime)
        {
            await Clients.All.SendAsync("CreateAvailableTime", availableTime);
        }

        public async Task UpdateAvailableTime(AvailableTime availableTime)
        {
            await Clients.All.SendAsync("UpdateAvailableTime", availableTime);
        }

        public async Task DeleteAvailableTime(AvailableTime availableTime)
        {
            await Clients.All.SendAsync("DeleteAvailableTime", availableTime);
        }
    }
}