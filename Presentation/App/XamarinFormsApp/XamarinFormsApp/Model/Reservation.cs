using System;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace XamarinFormsApp.Model
{
    public class Reservation : AutoMapper.Profile, IReservation<ReserveTime>
    {
        public Guid Id { get; set; }
        public ReserveTime Timeslot { get; set; }
        public Guid UserId { get; set; }
        public Guid ResourceId { get; set; }
    }
}