using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Mail.Domain.Events;

namespace UCLDreamTeam.Mail.Domain.EventHandlers
{
    public class ReservationCreatedEventHandler : IEventHandler<ReservationCreatedEvent>
    {

        public Task Handle(ReservationCreatedEvent @event)
        {
            _mailService.SendMail();
            return Task.FromResult(true);
        }
    }
}
