using Microsoft.AspNetCore.SignalR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR_Microservice.Hubs
{
    public class ResourceHub : Hub
    {
        public async Task UpdateReservationHub(Reservation reservation)
        {
            await Clients.All.SendAsync("UpdateReservationClient", reservation);

            //SignalR Client methods for UpdateReservation
            //_hubConnection.On<Reservation>("UpdateReservationClient", (reservation) =>
            //        {
            //            if (resource.Reservations.Find(r => r.Id = reservation.Id) != null)
            //            {
            //                resource.Reservations[resource.Reservations.FindId(r => r.Id = reservation.Id)] = reservation;
            //            }
            //            else
            //            {
            //                resource.Reservations.Add(reservation);
            //            }

            //        });

            //Task UpdateReservation() =>
            //        _hubConnection.SendAsync("UpdateReservationHub", _reservation);
        }

        //Change object to Reservation type when it is available from DB team
        public async Task UpdateResourceHub(Resource resource)
        {
            await Clients.All.SendAsync("UpdateResourceClient", resource);

            //SignalR Client methods for UpdateResource
            //_hubConnection.On<Resource>("UpdateResourceClient", (resource) =>
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
            //        _hubConnection.SendAsync("UpdateResourceHub", _resource);
        }
    }
}