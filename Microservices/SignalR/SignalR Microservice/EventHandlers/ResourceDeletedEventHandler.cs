using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Bus.Bus.Interfaces;
using SignalR_Microservice.Hubs;
using SignalR_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Microservice.EventHandlers
{
    public class ResourceDeletedEventHandler : IEventHandler<ResourceDeletedEvent>
    {
        private readonly IHubContext<ResourceHub> _hubContext;
        private readonly ILogger<ResourceDeletedEventHandler> _logger;

        public ResourceDeletedEventHandler(IHubContext<ResourceHub> hubContext,
            ILogger<ResourceDeletedEventHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task Handle(ResourceDeletedEvent @event)
        {
            _logger.LogInformation("ReservationCreatedEventHandler Called");
            await _hubContext.Clients.All.SendAsync("DeleteResource", @event.Resource);
        }
    }
}
