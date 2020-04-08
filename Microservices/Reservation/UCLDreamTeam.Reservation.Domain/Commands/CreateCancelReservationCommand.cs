using System;

namespace UCLDreamTeam.Reservation.Domain.Commands
{
    public class CreateCancelReservationCommand : CancelReservationCommand
    {
        public CreateCancelReservationCommand(Guid id)
        {
            Id = id;
        }
    }
}