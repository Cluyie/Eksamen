using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace AdminPanel.Client.Models
{
    public class Reservation : IReservation<ReserveTime>
    {
        public Guid Id { get; set; }
        public ReserveTime Timeslot { get; set; }
        public Guid UserId { get; set; }
        public Guid ResourceId { get; set; }
    }
}