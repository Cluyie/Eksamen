using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Interfaces;
using RabbitMQ.Bus.Events;
using UCLDreamTeam.Mail.Domain.Models;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Mail.Domain.Events
{
    public class UserRegisteredEvent : Event
    {
        public User User { get; }

        public UserRegisteredEvent(User user)
        {
            User = user;
        }   
    }
}
