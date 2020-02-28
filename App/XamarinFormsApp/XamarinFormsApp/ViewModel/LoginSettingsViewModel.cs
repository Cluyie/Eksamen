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
      var response = await _proxy.PostAsync(@"User/UpdateLogin", _mapper.Map<LoginSettings>(this));
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
