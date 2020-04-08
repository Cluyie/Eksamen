using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Bus.Commands;

namespace UCLDreamTeam.Reservation.Domain.Commands
{
    public class CancelReservationCommand : Command
    {
        public Guid Id { get; set; }
    }
}
