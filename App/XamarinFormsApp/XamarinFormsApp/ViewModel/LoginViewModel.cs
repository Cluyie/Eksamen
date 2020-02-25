using Autofac;
using AutoMapper;
using Business_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Model;

namespace XamarinFormsApp.ViewModel
{
  public class LoginViewModel : AutoMapper.Profile
  {
    #region Constructor
    private ApiClientProxy _proxy;
    private Mapper _mapper;

    public LoginViewModel()
    {
      _proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
      _mapper = AutofacHelper.Container.Resolve<Mapper>();
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
