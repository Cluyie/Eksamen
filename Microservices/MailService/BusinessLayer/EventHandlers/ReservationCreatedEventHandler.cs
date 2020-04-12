using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Models.Mail;
using RabbitMQ.Bus.Bus.Interfaces;
using RabbitMQ.Bus.Commands;
using UCLDreamTeam.Mail.Domain.Commands;
using UCLDreamTeam.Mail.Domain.Events;
using UCLDreamTeam.Mail.Domain.Models;
using UCLToolBox;

namespace UCLDreamTeam.Mail.Domain.EventHandlers
{
    public class ReservationCreatedEventHandler : IEventHandler<ReservationCreatedEvent>
    {
        private readonly IEventBus _eventBus;

        public ReservationCreatedEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(ReservationCreatedEvent @event)
        {
            var command = new SendEmailCommand(new MailModel
            {
                Template = Template.BookingConfirmation,
                Title = Template.BookingConfirmation.GetAttribute<DisplayAttribute>().Name,
                //Recipent =@event.UserId,
                //Reservation = @event.Id,
                //Resource = @event.ResourceId,
            });
            await _eventBus.SendCommand(command);
        }
    }

    public class GetRecipientInfoCommand<T> : Command where T : class
    {
        public Guid EventUserId { get; }

        public GetRecipientInfoCommand(Guid eventUserId)
        {
            EventUserId = eventUserId;
        }
    }

    public class GetReservationInfoCommand<T> : Command where T : class
    {
        public Guid EventId { get; }

        public GetReservationInfoCommand(Guid eventId)
        {
            EventId = eventId;
        }
    }

    public class GetResourceInfoCommand<T> : Command where T : class
    {
        public Guid EventResourceId { get; }

        public GetResourceInfoCommand(Guid eventResourceId)
        {
            EventResourceId = eventResourceId;
        }
    }
}
