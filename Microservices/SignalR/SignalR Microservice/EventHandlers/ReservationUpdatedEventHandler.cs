using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RabitMQEasy;
using SignalR_Microservice.Events;
using SignalR_Microservice.Hubs;
using SignalR_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Microservice.EventHandlers
{
    public class ReservationUpdatedEventHandler : ILissener<ReservationUpdatedEvent>
    {

        private readonly IHubContext<ReservationHub> _hubContext;
        private readonly ILogger<ReservationUpdatedEventHandler> _logger;

        public ReservationUpdatedEventHandler(IHubContext<ReservationHub> hubContext, ILogger<ReservationUpdatedEventHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task action(ReservationUpdatedEvent Obj)
        {
            _logger.LogInformation("ReservationUpdatedEventHandler Called");
            await _hubContext.Clients.All.SendAsync("UpdateReservation", Obj);
        }
    }
}
