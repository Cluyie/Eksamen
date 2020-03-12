using System;
using Data_Access_Layer.Context;
using Models;
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

        public ApiResponse<IReservation> Create(IReservation reservation)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<IReservation> Get(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}
