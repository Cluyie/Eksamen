using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Model;

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
      //throw new NotImplementedException();
      var response = await _proxy.PostAsync(@"Auth/UpdateLogin", _mapper.Map<LoginSettings>(this));
      var result = await ApiClientProxy.ReadAnswerAsync<ApiResponse<string>>(response);
      if (response.IsSuccessStatusCode)
      {
        Application.Current.Properties["token"] = result.Value;
      }
      else
      {
        ErrorMessage = Enum.GetName(typeof(ApiResponseCode), result.Code);
      }
      return response.IsSuccessStatusCode;
    }
  }
}
