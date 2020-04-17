using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Interfaces;
using UCLDreamTeam.Mail.Domain.Interfaces;

namespace UCLDreamTeam.Mail.Domain.Models
{
    public class User : IUser, IEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public string Address { get; set; }
        [NotMapped]
        public string City { get; set; }
        [NotMapped]
        public string Country { get; set; }
        [NotMapped]
        public int? ZipCode { get; set; }
        [NotMapped]
        public string PasswordHash { get; set; }
    }
}