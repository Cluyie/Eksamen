using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminPanel.Client.Models;

namespace AdminPanel.Client.DTOs
{
    public class TicketDTO
    {
        public Ticket Ticket { get; set; }
        public Reservation Reservation { get; set; }
        public ResourceDTO Resource { get; set; }
    }
}