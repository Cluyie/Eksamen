using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Models.Interfaces;

namespace UCLDreamTeam.User.Domain.Models
{
    public class User : IUser
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NormalizedUserName { get; set; }
        public string UserName { get; set; }
        public int? ZipCode { get; set; }
        [NotMapped]
        public string PasswordHash { get; set; }

    }
}
