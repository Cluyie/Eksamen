using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Bus.Events;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Mail.Domain.Events
{
    public class UserDeletedEvent : Event
    {
        public IUser User { get; }

        public UserDeletedEvent(IUser user)
        {
            User = user;
        }   
    }
}
