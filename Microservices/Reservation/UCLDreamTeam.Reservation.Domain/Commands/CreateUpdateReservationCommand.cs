using System;
using System.Collections.Generic;
using System.Text;
using UCLDreamTeam.Reservation.Domain.Models;

namespace UCLDreamTeam.Reservation.Domain.Commands
{
    public class CreateUpdateReservationCommand : UpdateReservationCommand
    {
        public CreateUpdateReservationCommand(Guid id, Guid userId, Guid resourceId, ReserveTime timeslot)
        {
            Id = id;
            UserId = userId;
            ResourceId = resourceId;
            Timeslot = timeslot;
        }
    }
}
