using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanelSeleniumTest.PageObjects
{
   public class SidebarPageObjects
    {
        IWebDriver driver;
        public SidebarPageObjects(IWebDriver _driver)
        {
            driver = _driver;
        }
        public IWebElement navRessource => driver.FindElement(By.Id("navRessourcer"));

        public IWebElement navLogout => driver.FindElement(By.Id("navLogout"));
    }
}
