using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Reservation.Domain.Commands;
using UCLDreamTeam.Reservation.Domain.Events;
using UCLDreamTeam.Reservation.Domain.Interfaces;

namespace UCLDreamTeam.Reservation.Domain.CommandHandlers
{
    public class ReservationCommandHandler : IRequestHandler<CreateReservationCommand, bool>
    {
        private readonly IEventBus _eventBus;
        private readonly IReservationRepository _reservationRepository;

        public ReservationCommandHandler(IEventBus eventBus, IReservationRepository reservationRepository)
        {
            _eventBus = eventBus;
            _reservationRepository = reservationRepository;
        }

        public async Task<bool> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _reservationRepository.AddAsync(new Models.Reservation
                {
                    Id = request.Id,
                    ResourceId = request.ResourceId,
                    Timeslot = request.Timeslot,
                    UserId = request.UserId
                });
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