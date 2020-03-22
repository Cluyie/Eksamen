using System;
using Data_Access_Layer.Context;
using System.Linq;
using Data_Access_Layer.Models;
using Business_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Business_Layer
{
    public class ReservationService
    {
        private ApplicationContext _applicationContext;

        public ReservationService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public ApiResponse<Reservation> Create(Reservation reservation)
        {
            Resource resourceToAddTo = _applicationContext.Resources
                    .Include(resource => resource.TimeSlots)
                    .Include(resource => resource.Reservations)
                    .ThenInclude(reservation => reservation.Timeslot)
                    .SingleOrDefault(resource => resource.Id == reservation.ResourceId);

            if (resourceToAddTo == null)
            {
                return new ApiResponse<Reservation>(ApiResponseCode.NoContent, reservation);
            }

            List<AvailableTime> validTimes = new List<AvailableTime>();
            resourceToAddTo.TimeSlots.ForEach(delegate(AvailableTime time)
            {
                if (time.From <= reservation.Timeslot.FromDate && time.To >= reservation.Timeslot.ToDate)
                {
                    validTimes.Add(time);
                }
            });

            if (validTimes.Any())
            {
                try
                {
                    _applicationContext.Add(reservation);
                    _applicationContext.SaveChanges();

                    return new ApiResponse<Reservation>(ApiResponseCode.OK, reservation);                
                }
                catch (Exception)
                {
                    return new ApiResponse<Reservation>(ApiResponseCode.InternalServerError, reservation);
                }
            }
            else
            {
                return new ApiResponse<Reservation>(ApiResponseCode.InternalServerError, reservation);
            }
        }

        public ApiResponse<Reservation> Get(Guid guid)
        {
            try
            {
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

        public ApiResponse<Reservation> Cancel(Guid guid)
        {
            try
            {
                var reservationToRemove = _applicationContext.Reservations.Find(guid);
                
                if (reservationToRemove != null)
                {
                    _applicationContext.Reservations.Remove(reservationToRemove);
                    _applicationContext.SaveChanges();

                    return new ApiResponse<Reservation>(ApiResponseCode.OK, null);
                }
                else
                {
                    return new ApiResponse<Reservation>(ApiResponseCode.NoContent, reservationToRemove);
                }
            }
            catch (Exception)
            {
                return new ApiResponse<Reservation>(ApiResponseCode.InternalServerError, null);
            }
        }
    }
}
