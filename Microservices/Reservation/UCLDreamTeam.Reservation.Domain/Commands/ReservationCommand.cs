using System;
using RabbitMQ.Bus.Commands;
using UCLDreamTeam.Reservation.Domain.Models;

namespace UCLDreamTeam.Reservation.Domain.Commands
{
    public class ReservationCommand : Command
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ResourceId { get; set; }
        public ReserveTime Timeslot { get; set; }
    }
}