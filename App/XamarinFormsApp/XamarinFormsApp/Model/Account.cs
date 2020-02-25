using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinFormsApp.Model
{
    public class Account : AutoMapper.Profile
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
