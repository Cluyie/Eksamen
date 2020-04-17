using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Interfaces;
using RabbitMQ.Bus.Events;

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
