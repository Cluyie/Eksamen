using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanelSeleniumTest.PageObjects
{
   public class AllRessourcesPageObjects
    {
        IWebDriver driver;
        public AllRessourcesPageObjects(IWebDriver _driver)
        {
            driver = _driver;
        }
        public IWebElement detailsBtn => driver.FindElement(By.Id("detailsBtn"));

    }
}
