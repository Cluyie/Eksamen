﻿using System;

namespace UCLDreamTeam.Auth.Api.Models.DTO
{
    public class CreateUserCredentialsDTO
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}