using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Bus.Bus.Interfaces;
using SignalR_Microservice.Events;
using SignalR_Microservice.Hubs;
using SignalR_Microservice.Models;

namespace SignalR_Microservice.EventHandlers
{
    public class ReservationCreatedEventHandler : IEventHandler<ReservationCreatedEvent>
    {
        private readonly IHubContext<ReservationHub> _hubContext;
        private readonly ILogger<ReservationCreatedEventHandler> _logger;

        public ReservationCreatedEventHandler(IHubContext<ReservationHub> hubContext,
            ILogger<ReservationCreatedEventHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task Handle(ReservationCreatedEvent @event)
        {
            _logger.LogInformation("ReservationCreatedEventHandler Called");
            await _hubContext.Clients.All.SendAsync("CreateReservation", @event);
        }
    }
}