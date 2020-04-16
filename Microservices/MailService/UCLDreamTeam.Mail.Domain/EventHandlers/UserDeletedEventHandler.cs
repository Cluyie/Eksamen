using System;
using System.Threading.Tasks;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Mail.Domain.Events;
using UCLDreamTeam.Mail.Domain.Interfaces;
using UCLDreamTeam.Mail.Domain.Models;

namespace UCLDreamTeam.Mail.Domain.EventHandlers
{
    public class UserDeletedEventHandler : IEventHandler<UserDeletedEvent>
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserDeletedEventHandler(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UserDeletedEvent @event)
        {
            await _userRepository.Update(@event.User.Id, new User
            {
                Email = @event.User.Email, 
                FirstName = @event.User.FirstName,
                LastName = @event.User.LastName
            });
        }
    }
}
