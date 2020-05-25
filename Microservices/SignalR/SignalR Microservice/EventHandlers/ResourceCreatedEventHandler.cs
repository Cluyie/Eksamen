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
    public class ResourceCreatedEventHandler : IEventHandler<ResourceCreatedEvent>
    {
        private readonly IHubContext<ResourceHub> _hubContext;
        private readonly ILogger<ResourceCreatedEventHandler> _logger;

        public ResourceCreatedEventHandler(IHubContext<ResourceHub> hubContext,
            ILogger<ResourceCreatedEventHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task Handle(ResourceCreatedEvent @event)
        {
            _logger.LogInformation("ReservationCreatedEventHandler Called");
            await _hubContext.Clients.All.SendAsync("CreateResource", @event.Resource);
        }
    }
}
