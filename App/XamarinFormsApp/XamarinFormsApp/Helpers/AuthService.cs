using System;
using Models;
using Xamarin.Forms;

namespace XamarinFormsApp.Helpers
{
    internal class AuthService
    {
        private ApiClientProxy _proxy;

        public AuthService(ApiClientProxy proxy)
        {
            _proxy = proxy;
        }

        public void Login(string token)
        {
            _proxy.httpClient.DefaultRequestHeaders.Clear();
            _proxy.httpClient.DefaultRequestHeaders.Add("Token", token);
            Application.Current.Properties["Token"] = token;
            UpdateUser();
        }

        public void UpdateUser(User user = null)
        {
            user ??= _proxy.Get<ApiResponse<User>>("User/GetProfile").Value;
            Application.Current.Properties["UserData"] = user;
        }
    }
}