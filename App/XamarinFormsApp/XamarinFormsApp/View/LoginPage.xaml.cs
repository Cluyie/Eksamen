﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.ViewModel;

namespace XamarinFormsApp.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class LoginPage : ContentPage
  {
    LoginViewModel _loginViewModel;
    private ApiClientProxy _proxy;

    public LoginPage()
    {
      InitializeComponent();
      BindingContext = _loginViewModel
          = new LoginViewModel();
      _proxy = DependencyService.Get<ApiClientProxy>();

    }

    async void OnLoginButtonClicked(object sender, EventArgs e)
    {

      _loginViewModel.Login();
      //if (!string.IsNullOrWhiteSpace(Account.Username) && !string.IsNullOrWhiteSpace(Account.Password))
      //{
      //    //send mig til login api
      //    //await Navigation.PushAsync(/*hjem eller bruger*/);
      //}
      //else
      //{
      //    //else
      //}

      ////
      await Navigation.PopAsync();
    }
  }
}