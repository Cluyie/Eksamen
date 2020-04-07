using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCLDreamTeam.Reservation.Domain.Models
{
    public class Reservation : IReservation<ReserveTime>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("Resource")] public Guid ResourceId { get; set; }
        public ReserveTime Timeslot { get; set; }
    }
}
