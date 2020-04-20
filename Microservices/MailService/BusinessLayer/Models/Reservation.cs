using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Interfaces;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Mail.Domain.Models
{
    public class Reservation : IReservation<ReserveTime>
    {
        public Guid Id { get; set; }
        public ReserveTime Timeslot { get; set; }
        public Guid UserId { get; set; }
        public Guid ResourceId { get; set; }

        //Mail local
        public User Recipent { get; set; }
        public Resource Resource { get; set; }
    }
}