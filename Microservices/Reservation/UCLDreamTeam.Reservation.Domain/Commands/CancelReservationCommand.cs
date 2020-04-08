using System;
using RabbitMQ.Bus.Commands;

namespace UCLDreamTeam.Reservation.Domain.Commands
{
    public class CancelReservationCommand : Command
    {
        public Guid Id { get; set; }
    }
}