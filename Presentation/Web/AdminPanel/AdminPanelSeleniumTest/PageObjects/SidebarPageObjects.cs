using OpenQA.Selenium;

namespace AdminPanelSeleniumTest.PageObjects
{
    public class SidebarPageObjects
    {
        private readonly IWebDriver _driver;

        public SidebarPageObjects(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement NavRessource => _driver.FindElement(By.Id("navRessourcer"));

        public IWebElement NavLogout => _driver.FindElement(By.Id("navLogout"));
    }
}