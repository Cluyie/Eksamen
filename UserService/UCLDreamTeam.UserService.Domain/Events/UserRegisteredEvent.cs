using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Bus.Events;
using UCLDreamTeam.UserServiceApi.Domain.Models;

namespace UCLDreamTeam.UserServiceApi.Domain.Events
{
    public class UserRegisteredEvent : Event
    {
        public User User { get; set; }

        public UserRegisteredEvent(User user)
        {
            User = user;
        }
    }
}
