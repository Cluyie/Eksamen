using System;
using Data_Access_Layer.Context;
using System.Linq;
using Data_Access_Layer.Models;
using Business_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class ReservationService
    {
        private ApplicationContext _applicationContext;
        private HubConnection _hubConnection;

        public ReservationService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _hubConnection = new HubConnectionBuilder()
            .WithUrl($"{Properties.Resources.ResourceManager.GetString("SignalRBaseAddress")}ReservationHub")
            .Build();
            Connect();
        }

        private async Task Connect()
        {
            await _hubConnection.StartAsync();
        }

        #region Create
        //Create a reservation
        public ApiResponse<Reservation> Create(Reservation reservation)
        {
            bool invalidDate = false;

            //Finds the resource that the reservation belongs to
            Resource resourceToAddTo = _applicationContext.Resources
                    .Include(resource => resource.TimeSlots)
                    .Include(resource => resource.Reservations)
                    .ThenInclude(reservation => reservation.Timeslot)
                    .SingleOrDefault(resource => resource.Id == reservation.ResourceId);

            if (resourceToAddTo == null)
            {
                return new ApiResponse<Reservation>(ApiResponseCode.NoContent, reservation);
            }

            resourceToAddTo.Reservations.ForEach(delegate(Reservation existingReservation)
            {
                if (!(((reservation.Timeslot.FromDate <= existingReservation.Timeslot.FromDate) && (reservation.Timeslot.ToDate <= existingReservation.Timeslot.FromDate)) ||
                ((reservation.Timeslot.FromDate >= existingReservation.Timeslot.ToDate) && (reservation.Timeslot.ToDate >= existingReservation.Timeslot.ToDate))))
                {
                    invalidDate = true;
                }
            });

            //A list of all the valid times for reservations duration
            List<AvailableTime> validTimes = new List<AvailableTime>();

            //Finds all the valid timeslots of the resource. The duration of the reservation must be equal to, or fit into an avaliable timeslot.
            resourceToAddTo.TimeSlots.ForEach(delegate(AvailableTime time)
            {
                if (time.From <= reservation.Timeslot.FromDate && time.To >= reservation.Timeslot.ToDate)
                {
                    validTimes.Add(time);
                }
            });

            //If the resource contains a valid timeslot, that matches the duration of the reservation
            if (validTimes.Any() && !invalidDate)
            {
                try
                {
                    _applicationContext.Add(reservation);
                    _applicationContext.SaveChanges();
                    _hubConnection.SendAsync("CreateReservation", reservation);

                    return new ApiResponse<Reservation>(ApiResponseCode.OK, reservation);                
                }
                catch (Exception)
                {
                    return new ApiResponse<Reservation>(ApiResponseCode.InternalServerError, null);
                }
            }
            else
            {
                return new ApiResponse<Reservation>(ApiResponseCode.NotModified, null);
            }
        }
        #endregion
        #region Get
        //Gets a reservation from a Guid
        public ApiResponse<Reservation> Get(Guid guid)
        {
            try
            {
                //Finds the reservation and asigns it, with all of its children
                var getReservation = _applicationContext.Reservations
                    .Include(reservation => reservation.Timeslot)
                    .FirstOrDefault(reservation => reservation.Id == guid);

                if (getReservation != null)
                {
                    return new ApiResponse<Reservation>(ApiResponseCode.OK, getReservation);
                }
                else
                {
                    return new ApiResponse<Reservation>(ApiResponseCode.NoContent, null);
                }

            }
            catch (Exception)
            {
                return new ApiResponse<Reservation>(ApiResponseCode.InternalServerError, null);
            }
        }
        #endregion
        #region Cancel
        //Cancel/Delete a reservation
        public ApiResponse<Reservation> Cancel(Guid guid)
        {
            try
            {
                //Makes sure the reservation is in the database, and asigns it.
                var reservationToRemove = _applicationContext.Reservations.Find(guid);
                
                if (reservationToRemove != null)
                {
                    _applicationContext.Reservations.Remove(reservationToRemove);
                    _applicationContext.SaveChanges();
                    _hubConnection.SendAsync("DeleteReservation", reservationToRemove);

                    return new ApiResponse<Reservation>(ApiResponseCode.OK, null);
                }
                else
                {
                    return new ApiResponse<Reservation>(ApiResponseCode.NoContent, null);
                }
            }
            catch (Exception)
            {
                return new ApiResponse<Reservation>(ApiResponseCode.InternalServerError, null);
            }
        }
        #endregion
    }
}
