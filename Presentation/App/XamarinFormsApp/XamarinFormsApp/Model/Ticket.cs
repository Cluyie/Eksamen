using System;
using System.Collections.Generic;
using System.Text;
using UCLDreamTeam.SharedInterfaces;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace XamarinFormsApp.Model
{
    public class Ticket : ITicket<IMessage, UserTicket>
    {
        public Guid Id { get; set; }
        public bool? Active { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }

        public List<IMessage> Messages { get; set; }

        public List<UserTicket> UserTickets { get; set; }

        public Guid? ReservationId { get; set; }
    }
}
