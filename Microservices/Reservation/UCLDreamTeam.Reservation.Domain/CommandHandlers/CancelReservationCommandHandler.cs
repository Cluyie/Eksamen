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
    public class CancelReservationCommandHandler : IRequestHandler<CreateCancelReservationCommand, bool>
    {
        private readonly IEventBus _eventBus;
        private readonly IReservationRepository _reservationRepository;

        public CancelReservationCommandHandler(IEventBus eventBus, IReservationRepository reservationRepository)
        {
            _eventBus = eventBus;
            _reservationRepository = reservationRepository;
        }

        public async Task<bool> Handle(CreateCancelReservationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _reservationRepository.CancelById(request.Id);
                _eventBus.PublishEvent(new ReservationCanceledEvent(request.Id));
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