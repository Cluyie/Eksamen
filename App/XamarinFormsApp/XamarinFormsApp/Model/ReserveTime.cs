using Models;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinFormsApp.Model
{
    public class ReserveTime : AutoMapper.Profile, IReserveTime
    {
        public Guid ReservationId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
