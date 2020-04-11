using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Bus.Bus.Interfaces;
using SignalR.Domain.Events;
using SignalR_Microservice.Hubs;
using SignalR_Microservice.Models;

namespace SignalR.Domain.EventHandlers
{
    public class ReservationCanceledEventHandler : IEventHandler<ReservationCanceledEvent>
    {
        private readonly IHubContext<ReservationHub> _hubContext;
        private readonly ILogger<ReservationCanceledEventHandler> _logger;

        public ReservationCanceledEventHandler(IHubContext<ReservationHub> hubContext,
            ILogger<ReservationCanceledEventHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task Handle(ReservationCanceledEvent @event)
        {
            _logger.LogInformation("ReservationCanceledEventHandler Called");
            await _hubContext.Clients.All.SendAsync("DeleteReservation", new Reservation {Id = @event.Id});
        }
    }
}