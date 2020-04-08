using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalR_Microservice.Models;

namespace SignalR_Microservice.Hubs
{
    public class ReservationHub : Hub
    {
        public async Task CreateReservation(Reservation reservation)
        {
            await Clients.All.SendAsync("CreateReservation", reservation);
        }

        public async Task UpdateReservation(Reservation reservation)
        {
            await Clients.All.SendAsync("UpdateReservation", reservation);
        }

        public async Task DeleteReservation(Reservation reservation)
        {
            await Clients.All.SendAsync("DeleteReservation", reservation);
        }
    }
}