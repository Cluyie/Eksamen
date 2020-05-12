using System;
using System.ComponentModel.DataAnnotations.Schema;
using UCLDreamTeam.SharedInterfaces.Interfaces;

namespace UCLDreamTeam.Auth.Api.Models.DTO
{
    public class CreateUserCredentialsDTO: IUser
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }
        [NotMapped]
        public string Address { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [NotMapped]
        public string City { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [NotMapped]
        public string Country { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [NotMapped]
        public string FirstName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [NotMapped]
        public string LastName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [NotMapped]
        public int? ZipCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}