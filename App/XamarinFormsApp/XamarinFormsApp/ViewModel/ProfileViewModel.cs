using Autofac;
using AutoMapper;
using System;
using System.Threading.Tasks;
using XamarinFormsApp.Helpers;
using Models;

namespace XamarinFormsApp.ViewModel
{
  public class ProfileViewModel : AutoMapper.Profile
  {
    #region Constructor
    private ApiClientProxy _proxy;
    private Mapper _mapper;

    public ProfileViewModel()
    {
      _proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
      _mapper = AutofacHelper.Container.Resolve<Mapper>();
    }
    #endregion

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public int? ZipCode { get; set; }
    public string Country { get; set; }

    public string ErrorMessage { get; private set; }


    public async Task<bool> UpdateProfile()
    {
      var user = _mapper.Map<User>(_mapper.Map<Profile>(this));
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
        
      }
      else
      {
        ErrorMessage = _proxy.GenerateErrorMessage(result, response);
      }
      return result?.Code == ApiResponseCode.OK;

    }
  }
}