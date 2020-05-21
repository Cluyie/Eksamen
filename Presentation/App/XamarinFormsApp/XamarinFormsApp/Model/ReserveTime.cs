using System;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace XamarinFormsApp.Model
{
    public class ReserveTime : AutoMapper.Profile, IReserveTime
    {
        public Guid ReservationId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}