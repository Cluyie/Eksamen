using AdminPanelSeleniumTest.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace AdminPanelSeleniumTest
{
    public class ResourceTests
    {
        //webdriveren igennem nugetpacket fungerer ikke hensigtsmæssigt.
        //En løsning er at hente en chromedriver her: https://chromedriver.chromium.org/ (sørg for at versionen stemmer overens med din webbrowser) 
        //installere den lokalt, og refere til den herunder:
        IWebDriver driver = new ChromeDriver(@"C:\Users\frede\Downloads\chromedriver");
        // Det ser ud til at det er nok bare at referere til mappen som chromedriver.exe ligger i, i hvert fald
        // hvis der kun ligger den fil i mappen

        LoginPageObjects _login;
        SidebarPageObjects _sidebar;
        AllRessourcesPageObjects _allResourcesPage;

        [SetUp]
        public void Setup()
        {
            _login = new LoginPageObjects(driver);
            _sidebar = new SidebarPageObjects(driver);
            _allResourcesPage = new AllRessourcesPageObjects(driver);
            driver.Navigate().GoToUrl("http://localhost:57346");
            WebDriverWait waitForElement = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            waitForElement.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("sidebar")));

            _login.Login("hejhejhej123", "1234");
        }


        [Test]
        public void AllRessources()
        {
           _sidebar.NavRessource.Click();
        }

        [Test]
        public void GoToRessourceDetail()
        {
            _sidebar.NavRessource.Click();
            _allResourcesPage.DetailsBtn.Click();
        }

        [TearDown]
        public void Logout()
        {
            _sidebar.NavLogout.Click();
        }


        [OneTimeTearDown]
        public void Close()
        {
            driver.Quit();
        }
    }
}