using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanelSeleniumTest.PageObjects
{
    public class AllRessourcesPageObjects
    {
        IWebDriver _driver;
        public AllRessourcesPageObjects(IWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement DetailsBtn => _driver.FindElement(By.Id("detailsBtn"));

        public IWebElement newRessource => _driver.FindElement(By.Id("newRessource"));
        public IWebElement editRessource => _driver.FindElement(By.Id("editRessource"));
        public IWebElement deleteRessource => _driver.FindElement(By.Id("deleteRessource"));



    }

}
