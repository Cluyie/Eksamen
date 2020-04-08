using System;
using OpenQA.Selenium;

namespace AdminPanelSeleniumTest.PageObjects
{
    public class EditRessourcePageObjects
    {
        private readonly IWebDriver _driver;

        public EditRessourcePageObjects(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement InputRessourceName => _driver.FindElement(By.Id("inputRessourceName"));

        public IWebElement AddTimeslot => _driver.FindElement(By.Id("addTimeslot"));

        public IWebElement DateFrom => _driver.FindElement(By.Id("dateFrom"));

        public IWebElement DateTo => _driver.FindElement(By.Id("dateTo"));

        public IWebElement Recurring => _driver.FindElement(By.Id("recurring"));

        public IWebElement Available => _driver.FindElement(By.Id("available"));
        public IWebElement RessourceSubmit => _driver.FindElement(By.Id("ressourceSubmit"));


        public void AddorEdit(string name, DateTime from, DateTime to, int rec, bool avail)
        {
            InputRessourceName.Clear();
            InputRessourceName.SendKeys(name);
            AddTimeslot.Click();
            DateFrom.SendKeys(from.ToString());
            DateTo.SendKeys(to.ToString());
            Recurring.Clear();
            Recurring.SendKeys(rec.ToString());
            if (!avail)
                Available.Click();
            RessourceSubmit.Click();
        }
    }
}