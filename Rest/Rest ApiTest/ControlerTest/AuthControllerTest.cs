using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rest_API.Controllers;
using System;
using System.Reflection;
using Business_Layer.Models;

namespace Rest_ApiTest
{
    [TestClass]
    public class AuthControllerTest
    {
        [TestMethod]
        [DataRow(new LoginDTO() {, Password = ""})]
        public void Login_LoginWithUserNameAndEnail()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\" + MethodBase.GetCurrentMethod().Name + "\\";
            AuthController authController = new AuthController();
            LoginDTO TestSubjekt = new LoginDTO() { };
            
            authController.Login();
        }
        [TestMethod]
        public void Login_LoginWithEmael()
        {
            
        }
        [TestMethod]
        public void Login_LoginWithUserName()
        {
            
        }
    }
}
