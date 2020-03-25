using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanelSeleniumTest.PageObjects
{
    public class EditRessourcePageObjects
    {
        IWebDriver _driver;
        public EditRessourcePageObjects(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement inputRessourceName => _driver.FindElement(By.Id("inputRessourceName"));

      public IWebElement addTimeslot => _driver.FindElement(By.Id("addTimeslot"));

        public IWebElement dateFrom => _driver.FindElement(By.Id("dateFrom"));

        public IWebElement dateTo => _driver.FindElement(By.Id("dateTo"));

        public IWebElement recurring => _driver.FindElement(By.Id("recurring"));

        public IWebElement available => _driver.FindElement(By.Id("available"));
        public IWebElement ressourceSubmit => _driver.FindElement(By.Id("ressourceSubmit"));


        public void AddorEdit(string name, DateTime from, DateTime to, int rec, bool avail)
        {
            inputRessourceName.Clear();
            inputRessourceName.SendKeys(name);
            addTimeslot.Click();
            dateFrom.SendKeys(from.ToString());
            dateTo.SendKeys(to.ToString());
            recurring.Clear();
            recurring.SendKeys(rec.ToString());
            if (!avail)
                available.Click();
            ressourceSubmit.Click();
        }
    }


   
}
