using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCLDreamTeam.SharedInterfaces.Interfaces
{
    public interface ITicket<TMessage> where TMessage : IMessage
    {
        public Guid Id { get; set; }
        public bool? Active { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public List<TMessage> Messages { get; set; }
        public Guid ReservationId { get; set; }
    }
}
