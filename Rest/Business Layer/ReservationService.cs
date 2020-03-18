using System;
using Data_Access_Layer.Context;
using System.Linq;
using Data_Access_Layer.Models;
using Business_Layer.Models;

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
            try
            {
                _applicationContext.Add(reservation);
                

                if (_applicationContext.SaveChanges() > 0)
                {
                    _applicationContext.SaveChanges();
                    return new ApiResponse<Reservation>(ApiResponseCode.OK, reservation);
                }
                else
                {
                    return new ApiResponse<Reservation>(ApiResponseCode.NotModified, reservation);
                }
                
            }
            catch (Exception)
            {
                return new ApiResponse<Reservation>(ApiResponseCode.InternalServerError, reservation);
            }
        }

        public ApiResponse<Reservation> Get(Guid guid)
        {
            try
            {
                var getReservation = _applicationContext.Reservations.Find(guid);

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
                
                if (reservationToRemove != null && _applicationContext.SaveChanges() > 0)
                {
                    _applicationContext.Reservations.Remove(reservationToRemove);
                    _applicationContext.SaveChanges();

                    return new ApiResponse<Reservation>(ApiResponseCode.OK, reservationToRemove);
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
