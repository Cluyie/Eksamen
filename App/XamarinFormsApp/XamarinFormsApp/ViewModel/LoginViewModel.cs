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
using UCLToolBox;

namespace XamarinFormsApp.ViewModel
{
  public class LoginViewModel : AutoMapper.Profile
  {
    #region Constructor
    private ApiClientProxy _proxy;
    private Mapper _mapper;
    private AuthService _authService;

    public LoginViewModel()
    {
      _proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
      _mapper = AutofacHelper.Container.Resolve<Mapper>();
      _authService = AutofacHelper.Container.Resolve<AuthService>();
    }
    #endregion

    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }

    public string ErrorMessage { get; private set; }

    /// <summary>
    /// Tries to register, returns a bool containing the result
    /// </summary>
    /// <returns></returns>
    public async Task<bool> Login()
    {
      var response = await _proxy.PostAsync(@"Auth/Login", _mapper.Map<Login>(this));
      var result = await ApiClientProxy.ReadAnswerAsync<ApiResponse<string>>(response);
      if (response.IsSuccessStatusCode && result?.Code == ApiResponseCode.OK)
      {
        //Gemmer user token
        _authService.Login(result.Value);
      }
      else
      {
        ErrorMessage = _proxy.GenerateErrorMessage(result, response);
      }
      return result?.Code == ApiResponseCode.OK;
    }
  }
}
