using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinFormsApp.Model
{
    public class Profile : AutoMapper.Profile
  {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int? ZipCode { get; set; }
        public string Country { get; set; }
    }
}
