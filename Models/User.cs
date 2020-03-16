﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
  public class User : IUser
  {
    public string Id { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string UserName { get; set; }
    public string Password { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public int ZipCode { get; set; }

    public string Country { get; set; }
  }
}