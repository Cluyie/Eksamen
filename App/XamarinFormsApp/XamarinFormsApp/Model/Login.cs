using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinFormsApp.Model
{
  public class Login : AutoMapper.Profile
  {
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
  }
}
