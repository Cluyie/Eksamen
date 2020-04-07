using System;
using System.Collections.Generic;
using System.Text;
using Models.Interfaces;

namespace XamarinFormsApp.Model
{
  public class Login : AutoMapper.Profile, ILoginDTO
  {
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
  }
}
