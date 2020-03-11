using Microsoft.AspNetCore.SignalR;
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
        public async Task UpdateReservation(object reservation)
        {
            await Clients.All.SendAsync("UpdateReservation", reservation);


            //SignalR Client methods for UpdateReservation
            //_hubConnection.On<object>("UpdateReservation", (reservation) =>
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
        public async Task UpdateResource(object resource)
        {
            await Clients.All.SendAsync("UpdateResource", resource);


            //SignalR Client methods for UpdateResource
            //_hubConnection.On<object>("UpdateResource", (resource) =>
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


