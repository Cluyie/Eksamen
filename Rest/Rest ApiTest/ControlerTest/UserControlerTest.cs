using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rest_API.Controllers;
using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Data_Access_Layer;

namespace Rest_ApiTest
{
    [TestClass]
    public class UserControlerTest
    {
        [TestMethod]
        public void UpdateUser_CreadUser()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\" + MethodBase.GetCurrentMethod().Name + "\\";
            
            UserController controler = new UserController();
            UserData TestSubjekt = LoadObjekt.Json<UserData>(path + "User");
            
            IActionResult TestObj = controler.UpdateUser(TestSubjekt., TestSubjekt);
            IActionResult resultat = LoadObjekt.Json<OkResult>(path + "Result");
            Assert.AreEqual(resultat, TestObj);
        }
        [TestMethod]
        public void UpdateUser_NotEnothUserInfo()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\" + MethodBase.GetCurrentMethod().Name + "\\";
            UserController controler = new UserController();
            UserData TestSubjekt = LoadObjekt.Json<UserData>(path + "User");

            IActionResult TestObj = controler.UpdateUser(TestSubjekt.ID, TestSubjekt);
            IActionResult resultat = LoadObjekt.Json<OkResult>(path + "Result");
            Assert.AreEqual(resultat, TestObj);
        }
    }
}
