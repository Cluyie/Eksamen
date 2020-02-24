using Xamarin.Forms;
using XamarinFormsApp.Model;

namespace XamarinFormsApp.ViewModel
{
  public class AccountViewModel
  {
    #region Constructor
    private ApiClientProxy _proxy;

    public AccountViewModel()
    {
      _proxy = DependencyService.Get<ApiClientProxy>();
    }
    #endregion

    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public async void Register()
    {
      var account = new Account { Username = Username, Email = Email, Password = Password };
      await _proxy.PostAsync("Register", account);
    }
  }
}