using Microsoft.AspNetCore.SignalR;
using SignalR_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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