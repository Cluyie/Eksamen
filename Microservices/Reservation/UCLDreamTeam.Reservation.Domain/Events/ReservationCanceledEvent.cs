using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Bus.Events;

namespace UCLDreamTeam.Reservation.Domain.Events
{
    public class ReservationCanceledEvent : Event
    {
        public Guid Id { get; set; }

        public ReservationCanceledEvent(Guid id)
        {
            Id = id;
        }

    }
}
