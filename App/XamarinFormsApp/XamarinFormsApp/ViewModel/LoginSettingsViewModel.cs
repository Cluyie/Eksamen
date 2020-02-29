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
      user.Id = new Guid("3c53f1b5-e6c6-470c-b725-80ca60d9f88d");
      user.Email = "test@mail.com";
      user.UserName = "username";
      user.FirstName = "firstname";
      user.LastName = "lastname";
      user.Address = "address";
      user.City = "city";
      user.ZipCode = 9999;
      user.Country = "country";
      var response = await _proxy.PutAsync(@"User/" + user.Id, user);
      var result = await ApiClientProxy.ReadAnswerAsync<ApiResponse<User>>(response);
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
