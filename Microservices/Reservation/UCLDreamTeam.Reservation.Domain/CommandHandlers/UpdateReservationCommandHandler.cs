using MediatR;
using RabbitMQ.Bus.Bus.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UCLDreamTeam.Reservation.Domain.Commands;
using UCLDreamTeam.Reservation.Domain.Events;
using UCLDreamTeam.Reservation.Domain.Interfaces;

namespace UCLDreamTeam.Reservation.Domain.CommandHandlers
{
    public class UpdateReservationCommandHandler : IRequestHandler<CreateUpdateReservationCommand, bool>
    {
        private readonly IEventBus _eventBus;
        private readonly IReservationRepository _reservationRepository;

        public UpdateReservationCommandHandler(IEventBus eventBus, IReservationRepository reservationRepository)
        {
            _eventBus = eventBus;
            _reservationRepository = reservationRepository;
        }

        public async Task<bool> Handle(CreateUpdateReservationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var reservation = new Models.Reservation
                {
                    Id = request.Id,
                    ResourceId = request.ResourceId,
                    Timeslot = request.Timeslot,
                    UserId = request.UserId
                };

                await _reservationRepository.UpdateAsync(reservation);
                _eventBus.PublishEvent(new ReservationUpdatedEvent(request.Id, request.UserId, request.ResourceId,
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
