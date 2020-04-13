using System;
using Models.Interfaces;
using UCLDreamTeam.Mail.Domain.Interfaces;

namespace UCLDreamTeam.Mail.Domain.Models
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}