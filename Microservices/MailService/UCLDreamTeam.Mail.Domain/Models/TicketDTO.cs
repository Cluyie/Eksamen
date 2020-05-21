using System;
using System.Collections.Generic;
using System.Text;

namespace UCLDreamTeam.Mail.Domain.Models
{
    public class TicketDTO
    {
        public Ticket Ticket { get; }
        public Reservation Reservation { get; set; }
        public Resource Resource { get; set; }

        public TicketDTO()
        {

        }

        public TicketDTO(Ticket ticket, Reservation reservation, Resource resource)
        {
            Ticket = ticket;
            Reservation = reservation;
            Resource = resource;
        }
    }
}
