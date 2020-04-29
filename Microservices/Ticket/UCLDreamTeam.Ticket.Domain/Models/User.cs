using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Ticket.Domain.Models
{
    public class User : IUser
    {
        public Guid Id { get; set; }
        public List<UserTicket> UserTickets { get; set; }

        [NotMapped]
        public string Address { get; set; }
        [NotMapped]
        public string City { get; set; }
        [NotMapped]
        public string Country { get; set; }
        [NotMapped]
        public string Email { get; set; }
        [NotMapped]
        public string FirstName { get; set; }
        [NotMapped]
        public string LastName { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public int? ZipCode { get; set; }
        [NotMapped]
        public string PasswordHash { get; set; }
    }
}
