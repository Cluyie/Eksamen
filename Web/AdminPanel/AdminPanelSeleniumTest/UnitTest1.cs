using AdminPanelSeleniumTest.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace AdminPanelSeleniumTest
{
    public class Tests
    {
        //webdriveren igennem nugetpacket fungerer ikke hensigtsmæssigt.
        //En løsning er at hente en chromedriver her: https://chromedriver.chromium.org/ (sørg for at versionen stemmer overens med din webbrowser) 
        //installere den lokalt, og refere til den herunder:
        IWebDriver driver = new ChromeDriver(@"C:\Users\User\Source\repos\lasserasch\UCLDreamTeam\Web\AdminPanel\AdminPanelSeleniumTest");

        LoginPageObjects login;
        SidebarPageObjects sidebar;
        AllRessourcesPageObjects allRessourcesPage;
           

        [SetUp]
        public void Setup()
        {

            login = new LoginPageObjects(driver);
            sidebar = new SidebarPageObjects(driver);
            allRessourcesPage = new AllRessourcesPageObjects(driver);
            driver.Navigate().GoToUrl("http://localhost:57346");
            WebDriverWait waitForElement = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            waitForElement.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("sidebar")));

            login.Login("hejhejhej123", "1234");
        }


        [Test]
        public void AllRessources()
        {
           
           sidebar.navRessource.Click();
           
        }

        [Test]
        public void goToRessourceDetail()
        {
            
            sidebar.navRessource.Click();
            allRessourcesPage.detailsBtn.Click();
            
        }

        [TearDown]
        public void Logout()
        {
            sidebar.navLogout.Click();
        }


        [OneTimeTearDown]
        public void Close()
        {
            driver.Quit();
        }
    }
}