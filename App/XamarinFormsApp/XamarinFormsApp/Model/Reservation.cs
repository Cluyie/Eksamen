using System;
using System.Collections.Generic;
using System.Text;
using Models.Interfaces;

namespace XamarinFormsApp.Model
{
    public class Reservation<TReservation> : AutoMapper.Profile, IReservation<TReservation> where TReservation : IReserveTime
    {
        public Guid Id { get ; set ; }
        public TReservation Timeslot { get ; set ; }
        public Guid UserId { get ; set ; }
        public Guid ResourceId { get ; set ; }
    }
}
