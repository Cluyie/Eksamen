using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Interfaces;

namespace AdminPanel.Client.Models
{
    public class Ticket : ITicket<Message>
    {
        public Guid Id { get; set; }
        public bool? Active { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public List<Message> Messages { get; set; }
        public Guid ReservationId { get; set; }
    }
}