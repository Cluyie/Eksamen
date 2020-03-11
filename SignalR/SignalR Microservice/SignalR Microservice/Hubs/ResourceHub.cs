using Microsoft.AspNetCore.SignalR;
<<<<<<< Updated upstream
=======
using Models;
>>>>>>> Stashed changes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR_Microservice.Hubs
{
    public class ResourceHub : Hub
    {
        //Change object to Reservation type when it is available from DB team
<<<<<<< Updated upstream
        public async Task UpdateReservation(object reservation)
=======
        public async Task UpdateReservation(Reservation reservation)
>>>>>>> Stashed changes
        {
            await Clients.All.SendAsync("UpdateReservation", reservation);


            //SignalR Client methods for UpdateReservation
<<<<<<< Updated upstream
            //_hubConnection.On<object>("UpdateReservation", (reservation) =>
=======
            //_hubConnection.On<Reservation>("UpdateReservation", (reservation) =>
>>>>>>> Stashed changes
            //        {
            //            if (resource.Reservations.Find(r => r.Id = reservation.Id) != null)
            //	        {
            //                resource.Reservations[resource.Reservations.FindId(r => r.Id = reservation.Id)] = reservation;
            //	        }
            //            else
            //            {
            //                resource.Reservations.Add(reservation);
            //            }

            //        });

            //Task UpdateReservation() =>
            //        _hubConnection.SendAsync("UpdateReservation", _reservation);
        }

        //Change object to Reservation type when it is available from DB team
<<<<<<< Updated upstream
        public async Task UpdateResource(object resource)
=======
        public async Task UpdateResource(Resource resource)
>>>>>>> Stashed changes
        {
            await Clients.All.SendAsync("UpdateResource", resource);


            //SignalR Client methods for UpdateResource
<<<<<<< Updated upstream
            //_hubConnection.On<object>("UpdateResource", (resource) =>
=======
            //_hubConnection.On<Resource>("UpdateResource", (resource) =>
>>>>>>> Stashed changes
            //        {
            //            if (resourceList.Find(r => r.Id == resource.Id) != null)
            //            {
            //                resourceList[resourceList.FindId(r => r.Id = resource.Id)] = resource;
            //            }
            //            else
            //            {
            //                resourceList.Add(resource);
            //            }
            //        });

            //Task UpdateResource() =>
            //        _hubConnection.SendAsync("UpdateResource", _resource);
        }
    }
}


