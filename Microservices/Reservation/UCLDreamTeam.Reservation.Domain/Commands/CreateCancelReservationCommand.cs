using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCLDreamTeam.Reservation.Domain.Commands
{
    public class CreateCancelReservationCommand :CancelReservationCommand
    {
        public CreateCancelReservationCommand(Guid id)
        {
            Id = id;
        }
    }
}
