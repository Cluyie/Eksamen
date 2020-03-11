using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace XamarinFormsApp.Model
{
    public class Reservation : AutoMapper.Profile, IReservation
    {
        public Guid Id { get; set; }
        public List<IReserveTime> Timeslot { get; set; }
        public Guid UserId { get; set; }
    }
}
