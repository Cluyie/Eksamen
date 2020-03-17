using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Models.Interfaces;

namespace Data_Access_Layer.Models
{
    public class User : IdentityUser, IUser
    {
        public override string Email { get; set; }
        public override string UserName { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public int? ZipCode { get; set; }

        public string Country { get; set; }
    }
}
