using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace AdminPanel.Client.Models
{
    public class Message : IMessage
    {
        public Guid Id { get; set; }
        [ForeignKey("Ticket")]
        public Guid TicketId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Seen { get; set; } = false;
    }
}