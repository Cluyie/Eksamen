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
    public class ResourceUpdatedEventHandler : IEventHandler<ResourceUpdatedEvent>
    {
        private readonly IHubContext<ResourceHub> _hubContext;
        private readonly ILogger<ResourceUpdatedEventHandler> _logger;

        public ResourceUpdatedEventHandler(IHubContext<ResourceHub> hubContext,
            ILogger<ResourceUpdatedEventHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task Handle(ResourceUpdatedEvent @event)
        {
            _logger.LogInformation("ReservationCreatedEventHandler Called");
            await _hubContext.Clients.All.SendAsync("UpdateResource", @event.Resource);
        }
    }
}
