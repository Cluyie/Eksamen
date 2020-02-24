using System;
using Xamarin.Forms;
using XamarinFormsApp.Model;

namespace XamarinFormsApp.ViewModel
{
  public class ProfileViewModel
  {
    #region Constructor
    private ApiClientProxy _proxy;

    public ProfileViewModel()
    {
      _proxy = DependencyService.Get<ApiClientProxy>();
    }
    #endregion

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public int? ZipCode { get; set; }
    public string Country { get; set; }

    public async void UpdateProfile()
    {
      var profile = new Profile { FirstName = FirstName, LastName = LastName, Address = Address, City = City, ZipCode = ZipCode, Country = Country };
      await _proxy.PostAsync("1", profile);
    }
  }
}