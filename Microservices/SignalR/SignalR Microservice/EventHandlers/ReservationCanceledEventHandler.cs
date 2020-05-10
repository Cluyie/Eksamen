using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RabitMQEasy;
using SignalR_Microservice.Events;
using SignalR_Microservice.Hubs;
using SignalR_Microservice.Models;

namespace SignalR_Microservice.EventHandlers
{
    public class ReservationCanceledEventHandler : ILissener<ReservationCanceledEvent>
    {
        private readonly IHubContext<ReservationHub> _hubContext;
        private readonly ILogger<ReservationCanceledEventHandler> _logger;

        public ReservationCanceledEventHandler(IHubContext<ReservationHub> hubContext,
            ILogger<ReservationCanceledEventHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task action(ReservationCanceledEvent Obj)
        {
            _logger.LogInformation("ReservationCanceledEventHandler Called");
            await _hubContext.Clients.All.SendAsync("DeleteReservation", Obj.Id);
        }

    }
}