using Microsoft.AspNetCore.SignalR;
using RabitMQEasy;
using SignalR_Microservice.Hubs;
using SignalR_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Microservice.EventHandlers
{
    public class ResourceDeleadetEventHandler : IEventHandler<Resource>
    {
        public RabitMQEasy.Events events { get; set; } = RabitMQEasy.Events.DeleateObject;
        IHubContext<ResourceHub> HubContext;
        public ResourceDeleadetEventHandler(IHubContext<ResourceHub> hubContext)
        {
            HubContext = hubContext;
        }

        public async Task action(Resource Obj)
        {
            await HubContext.Clients.All.SendAsync("DeleteResource", Obj);
        }
    }
}
