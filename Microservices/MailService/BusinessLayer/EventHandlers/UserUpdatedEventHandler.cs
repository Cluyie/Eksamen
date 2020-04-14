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
    public class UserUpdatedEventHandler : IEventHandler<UserUpdatedEvent>
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserUpdatedEventHandler(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UserUpdatedEvent @event)
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
