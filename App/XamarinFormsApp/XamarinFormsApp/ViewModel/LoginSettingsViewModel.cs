using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Model;
using Models;

namespace XamarinFormsApp.ViewModel
{
  public class LoginSettingsViewModel : AutoMapper.Profile
  {
    #region Constructor
    private ApiClientProxy _proxy;
    private Mapper _mapper;

    public LoginSettingsViewModel()
    {
      _proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
      _mapper = AutofacHelper.Container.Resolve<Mapper>();
    }
    #endregion

    public string Email { get; set; }
    public string Password { get; set; }

    public string ErrorMessage { get; private set; }

    public async Task<bool> UpdateLogin()
    {
      var user = _mapper.Map<User>(_mapper.Map<LoginSettings>(this));
      user.Id = new Guid("92cffb29-7699-4bbf-8da6-560c6f3bfdfb");
      user.Email = "chriskpedersen@hotmail.com";
      user.UserName = "Tonur";
      user.FirstName = "Christoffer";
      user.LastName = "Pedersen";
      user.Address = "Østerbrogade 20, 2 Th.";
      user.City = "Vejle";
      user.ZipCode = 7100;
      user.Country = "Denmark";
      var response = await _proxy.PutAsync(@"User/" + user.Id, user);
      var result = await ApiClientProxy.ReadAnswerAsync<ApiResponse<string>>(response);
      if (response.IsSuccessStatusCode && result?.Code == ApiResponseCode.OK)
      {
        Application.Current.Properties["token"] = result.Value;
      }
      else
      {
        ErrorMessage = _proxy.GenerateErrorMessage(result, response);
      }
      return result?.Code == ApiResponseCode.OK;
    }
  }
}
