using System;
using System.Threading.Tasks;
using Models.Mail;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Mail.Domain.Commands;
using UCLDreamTeam.Mail.Domain.Events;
using UCLDreamTeam.Mail.Domain.Interfaces;
using UCLDreamTeam.Mail.Domain.Models;

namespace UCLDreamTeam.Mail.Domain.EventHandlers
{
    public class ReservationCreatedEventHandler : IEventHandler<ReservationCreatedEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Resource> _resourceRepository;

        public ReservationCreatedEventHandler(IEventBus eventBus, IGenericRepository<User> userRepository, IGenericRepository<Resource> resourceRepository)
        {
            _eventBus = eventBus;
            _userRepository = userRepository;
            _resourceRepository = resourceRepository;
        }

        public async Task Handle(ReservationCreatedEvent @event)
        {
            var command = new SendEmailCommand(new Reservation
            {
                Recipent = await _userRepository.GetById(@event.UserId),
                Resource = await _resourceRepository.GetById(@event.ResourceId),
                Timeslot = @event.Timeslot
            }, Template.BookingConfirmation);
            await _eventBus.SendCommand(command);
        }
    }
}
