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

        public IWebElement NewRessource => _driver.FindElement(By.Id("newRessource"));
        public IWebElement EditRessource => _driver.FindElement(By.Id("editRessource"));
        public IWebElement DeleteRessource => _driver.FindElement(By.Id("deleteRessource"));



    }

}
