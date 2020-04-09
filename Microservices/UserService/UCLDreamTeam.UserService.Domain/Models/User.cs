using System;
using Microsoft.AspNetCore.Identity;
using Models.Interfaces;

namespace UCLDreamTeam.UserServiceApi.Domain.Models
{
    public class User : IdentityUser<Guid>, IUser
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? ZipCode { get; set; }
    }
}