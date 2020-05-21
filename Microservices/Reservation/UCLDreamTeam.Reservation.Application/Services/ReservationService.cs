using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Reservation.Application.Interfaces;
using UCLDreamTeam.Reservation.Domain.Commands;
using UCLDreamTeam.Reservation.Domain.Interfaces;

namespace UCLDreamTeam.Reservation.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IEventBus _eventBus;
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IEventBus eventBus, IReservationRepository reservationRepository)
        {
            _eventBus = eventBus;
            _reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<Domain.Models.Reservation>> GetAsync()
        {
            return await _reservationRepository.GetAsync();
        }

        public async Task<Domain.Models.Reservation> GetByIdAsync(Guid id)
        {
            return await _reservationRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Domain.Models.Reservation>> GetByResourceId(Guid resourceId)
        {
            return await _reservationRepository.GetByResourceId(resourceId);
        }

        public async Task AddAsync(Domain.Models.Reservation reservation)
        {
            var command = new CreateReservationCommand(reservation.Id, reservation.UserId, reservation.ResourceId,
                reservation.Timeslot);
            await _eventBus.SendCommand(command);
        }

        public async Task UpdateAsync(Domain.Models.Reservation reservation)
        {
            var command = new CreateUpdateReservationCommand(reservation.Id, reservation.UserId, reservation.ResourceId, reservation.Timeslot);
            await _eventBus.SendCommand(command);
        }

        public async Task CancelById(Guid id)
        {
            var command = new CreateCancelReservationCommand(id);
            await _eventBus.SendCommand(command);
        }
    }
}