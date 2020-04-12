using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Bus.Bus.Interfaces;
using UCLDreamTeam.Mail.Domain.Events;
using UCLDreamTeam.Mail.Domain.Interfaces;
using UCLDreamTeam.Mail.Domain.Models;

namespace UCLDreamTeam.Mail.Domain.EventHandlers
{
    public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserCreatedEventHandler(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UserCreatedEvent @event)
        {
            await _userRepository.Create(new User
            {
                Id = @event.User.Id,
                Email = @event.User.Email, 
                FirstName = @event.User.FirstName,
                LastName = @event.User.LastName
            });
        }
    }
}
