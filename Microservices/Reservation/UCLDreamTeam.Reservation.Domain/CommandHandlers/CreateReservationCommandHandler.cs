using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Reservation.Domain.Commands;
using UCLDreamTeam.Reservation.Domain.Events;
using UCLDreamTeam.Reservation.Domain.Interfaces;

namespace UCLDreamTeam.Reservation.Domain.CommandHandlers
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, bool>
    {
        private readonly IEventBus _eventBus;
        private readonly IReservationRepository _reservationRepository;
        private readonly ILogger<CreateReservationCommandHandler> _logger;

        public CreateReservationCommandHandler(IEventBus eventBus, IReservationRepository reservationRepository,
            ILogger<CreateReservationCommandHandler> logger)
        {
            _eventBus = eventBus;
            _reservationRepository = reservationRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
                _logger.LogInformation("CreateReservationCommandHandler Called");
            try
            {
                var reservation = new Models.Reservation
                {
                    Id = request.Id,
                    ResourceId = request.ResourceId,
                    Timeslot = request.Timeslot,
                    UserId = request.UserId
                };

                await _reservationRepository.AddAsync(reservation);
                _eventBus.PublishEvent(new ReservationCreatedEvent(request.Id, request.UserId, request.ResourceId,
                    request.Timeslot));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return await Task.FromResult(true);
        }
    }
}