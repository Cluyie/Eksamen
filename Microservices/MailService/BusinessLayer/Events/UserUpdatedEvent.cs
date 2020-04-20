using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Interfaces;
using RabbitMQ.Bus.Events;
using UCLDreamTeam.Mail.Domain.Models;

namespace UCLDreamTeam.Mail.Domain.Events
{
    public class UserUpdatedEvent : Event
    {
        public User User { get; }

        public UserUpdatedEvent(User user)
        {
            User = user;
        }   
    }
}
