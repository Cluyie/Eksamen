using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinFormsApp.Model
{
  public class LoginSettings : AutoMapper.Profile
  {
    public string Email { get; set; }
    public string Password { get; set; }
  }
}
