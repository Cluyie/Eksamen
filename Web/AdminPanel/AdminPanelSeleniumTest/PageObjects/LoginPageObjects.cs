using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

using SeleniumExtras.PageObjects;
using AdminPanelSeleniumTest.PageObjects;

namespace AdminPanelSeleniumTest
{
    public class LoginPageObjects
    {

        IWebDriver driver;
        public LoginPageObjects(IWebDriver _driver)
        {
            driver = _driver;
        }

        public IWebElement navLogin => driver.FindElement(By.Id("navLogin"));
        public IWebElement loginUsername => driver.FindElement(By.Id("login-username"));
        public IWebElement loginPassword => driver.FindElement(By.Id("login-password"));
        public IWebElement loginSubmit => driver.FindElement(By.Id("loginSubmit"));


        public void Login(string username, string password)
        {
            navLogin.Click();
            loginUsername.SendKeys(username);
            loginPassword.SendKeys(password);
            loginSubmit.Click();
        }





        


    }
}
