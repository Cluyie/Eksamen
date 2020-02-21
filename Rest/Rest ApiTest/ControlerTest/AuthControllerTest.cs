using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rest_API.Controllers;
using System;
using System.Reflection;
using Business_Layer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Rest_ApiTest
{
    [TestClass]
    public class AuthControllerTest
    {
        [TestMethod]
        //Login with Email
        [DataRow("Hallo@test.com", "Pasword")]
        //Login wiht UserName
        [DataRow("Bruger","Pasword")]
        public void Login_LoginWithUserNameAndEnail(string UsernameOrEmail, string Psaward)
        {
            AuthController authController = new AuthController();
            LoginDTO TestSubjekt = new LoginDTO() {UsernameOrMail = UsernameOrEmail, Password = Psaward };
            ApiResponse<string> returnObj = authController.Login(TestSubjekt);

        }
        [TestMethod]
        [DataRow("Hallotest.com", "Pasword")]
        [DataRow("Hallo@test.com", "Pasword")]
        [DataRow("Hallo@test.com", "Pasword.e")]
        [DataRow("Brugere", "Pasword")]
        public void Login_WornLoginInformation(string UsernameOrEmail, string Psaward)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\" + MethodBase.GetCurrentMethod().Name + "\\";
            AuthController authController = new AuthController();
            LoginDTO TestSubjekt = new LoginDTO() { UsernameOrMail = UsernameOrEmail, Password = Psaward };
            //string returnObj = authController.Login(TestSubjekt);

        }
    }
}
