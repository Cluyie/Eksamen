using Autofac;
using AutoMapper;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Model;

namespace XamarinFormsApp.ViewModel
{
  public class AccountViewModel : AutoMapper.Profile
  {
    #region Constructor
    private ApiClientProxy _proxy;
    private Mapper _mapper;

    public AccountViewModel()
    {
      _proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
      _mapper = AutofacHelper.Container.Resolve<Mapper>();
    }
    #endregion

    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public string ErrorMessage { get; set; }


    /// <summary>
    /// Tries to register, returns a bool containing the result
    /// </summary>
    /// <returns></returns>
    public async Task<bool> Register()
    {
      var response = await _proxy.PostAsync(@"Auth/Register", _mapper.Map<Account>(this));
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