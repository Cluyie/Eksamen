using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanelSeleniumTest.PageObjects
{
   public class SidebarPageObjects
    {
        IWebDriver _driver;
        public SidebarPageObjects(IWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement NavRessource => _driver.FindElement(By.Id("navRessourcer"));

        public IWebElement NavLogout => _driver.FindElement(By.Id("navLogout"));
    }
}
