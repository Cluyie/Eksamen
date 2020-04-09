using OpenQA.Selenium;

namespace AdminPanelSeleniumTest.PageObjects
{
    public class LoginPageObjects
    {
        private readonly IWebDriver _driver;

        public LoginPageObjects(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement NavLogin => _driver.FindElement(By.Id("navLogin"));
        public IWebElement LoginUsername => _driver.FindElement(By.Id("login-username"));
        public IWebElement LoginPassword => _driver.FindElement(By.Id("login-password"));
        public IWebElement LoginSubmit => _driver.FindElement(By.Id("loginSubmit"));


        public void Login(string username, string password)
        {
            NavLogin.Click();
            LoginUsername.SendKeys(username);
            LoginPassword.SendKeys(password);
            LoginSubmit.Click();
        }
    }
}