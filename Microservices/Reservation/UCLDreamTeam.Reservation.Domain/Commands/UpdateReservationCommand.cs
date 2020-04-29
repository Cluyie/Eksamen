using RabbitMQ.Bus.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using UCLDreamTeam.Reservation.Domain.Models;

namespace UCLDreamTeam.Reservation.Domain.Commands
{
    public class UpdateReservationCommand : Command
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ResourceId { get; set; }
        public ReserveTime Timeslot { get; set; }
    }
}
