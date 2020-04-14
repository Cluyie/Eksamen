using System;
using Models.Interfaces;
using UCLDreamTeam.Reservation.Domain.Models;

namespace UCLDreamTeam.Reservation.Domain.Commands
{
    public class CreateReservationCommand : ReservationCommand
    {
        public CreateReservationCommand(Guid id, Guid userId, Guid resourceId, ReserveTime timeslot)
        {
            Id = id;
            UserId = userId;
            ResourceId = resourceId;
            Timeslot = timeslot;
        }
    }
}