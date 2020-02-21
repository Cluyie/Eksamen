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
            UserData TestSubjekt = new UserData() { Id = Guid.NewGuid(), Username = "UserName", Email = "Hallo@test.com", Address = "Adventre st. 15", City = "New York", Country = "USA", FirstName = "Tony", LastName = "Stark", ZipCode = "10041" };


            var TestObj = controler.UpdateUser(TestSubjekt.Id, TestSubjekt);
            //IActionResult resultat = LoadObjekt.Json<OkResult>(path + "Result");
            Assert.AreEqual(TestSubjekt, TestObj.Value);
        }
        [TestMethod]
        public void UpdateUser_NotEnothUserInfo()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\" + MethodBase.GetCurrentMethod().Name + "\\";
            UserController controler = new UserController();
            UserData TestSubjekt = new UserData() { Id = Guid.NewGuid(), Username = "UserName", Email = "Hallo@test.com", Address = "Adventre st. 15", City = "New York", Country = "USA", FirstName = "Tony", LastName = "Stark", ZipCode = "10041" };

            var TestObj = controler.UpdateUser(TestSubjekt.Id, TestSubjekt);
            //IActionResult resultat = LoadObjekt.Json<OkResult>(path + "Result");
            Assert.AreEqual(TestSubjekt, TestObj.Value);
        }
    }
}
