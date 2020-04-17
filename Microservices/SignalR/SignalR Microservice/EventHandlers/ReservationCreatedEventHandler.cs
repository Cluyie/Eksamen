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
            await _hubContext.Clients.All.SendAsync("CreateReservation", new Reservation
            {
                Id = @event.Id,
                ResourceId = @event.ResourceId,
                Timeslot = @event.Timeslot,
                UserId = @event.UserId
            });
        }
    }
}