using System;
using System.Collections.Generic;
using AdminPanel.Client.Services;
using AdminPanelSeleniumTest.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AdminPanelSeleniumTest
{
    public class ResourceTests
    {
        private AllRessourcesPageObjects _allResourcesPage;
        private EditRessourcePageObjects _editRessourcePageObjects;

        private LoginPageObjects _login;
        // Det ser ud til at det er nok bare at referere til mappen som chromedriver.exe ligger i, i hvert fald
        // hvis der kun ligger den fil i mappen

        private readonly MockResourceService _resourceService = new MockResourceService();

        private SidebarPageObjects _sidebar;

        //webdriveren igennem nugetpacket fungerer ikke hensigtsmæssigt.
        //En løsning er at hente en chromedriver her: https://chromedriver.chromium.org/ (sørg for at versionen stemmer overens med din webbrowser) 
        //installere den lokalt, og refere til den herunder:
        private readonly IWebDriver driver = new ChromeDriver(@"C:\Users\frede\Downloads\chromedriver");
        private WebDriverWait waitForElement;

        [SetUp]
        public void Setup()
        {
            _login = new LoginPageObjects(driver);
            _sidebar = new SidebarPageObjects(driver);
            _allResourcesPage = new AllRessourcesPageObjects(driver);
            _editRessourcePageObjects = new EditRessourcePageObjects(driver);
            waitForElement = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            driver.Navigate().GoToUrl("http://localhost:57346");

            waitForElement.Until(ExpectedConditions.ElementIsVisible(By.ClassName("sidebar")));

            _login.Login("tonur", " ");
        }


        [Test]
        public void GoToRessourceDetail()
        {
            wait();
            _sidebar.NavRessource.Click();
            _allResourcesPage.DetailsBtn.Click();
        }

        [Test]
        public void AddRessource()
        {
            var beforeCount = _resourceService.GetCount();
            var expected = beforeCount + 1;


            displayRessources(false);
            Console.WriteLine();
            _sidebar.NavRessource.Click();
            _allResourcesPage.NewRessource.Click();

            _editRessourcePageObjects.AddorEdit("Trailer", new DateTime(2020, 4, 14), new DateTime(2020, 4, 24), 7,
                true);

            var afterCount = countRessources();


            displayRessources(true);


            Assert.AreEqual(expected, afterCount);
        }

        [Test]
        public void DeleteRessource()
        {
            var beforeCount = _resourceService.GetCount();
            var expected = beforeCount - 1;
            displayRessources(false);
            _sidebar.NavRessource.Click();

            Console.WriteLine("attempting to delete: " + driver.FindElement(By.Id("ressourceName")).Text);
            _allResourcesPage.DeleteRessource.Click();
            driver.FindElement(By.Id("confirmDelete")).Click();


            var afterCount = countRessources();
            displayRessources(true);

            Assert.AreEqual(expected, afterCount);
        }

        [Test]
        public void EditRessource()
        {
            displayRessources(false);
            _sidebar.NavRessource.Click();
            Console.WriteLine("Will attempt to edit: " + driver.FindElement(By.Id("ressourceName")).Text);
            _allResourcesPage.EditRessource.Click();

            _editRessourcePageObjects.AddorEdit("Lokale 34", new DateTime(2020, 4, 14), new DateTime(2020, 4, 24), 4,
                true);
            displayRessources(true);
        }

        [Test]
        public void AllRessources()
        {
            displayRessources(true);
        }

        public void wait()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        public int countRessources()
        {
            _sidebar.NavRessource.Click();
            var NtTable = driver.FindElement(By.ClassName("table"));
            IReadOnlyCollection<IWebElement> TableRows = NtTable.FindElements(By.Id("tableRow"));
            return TableRows.Count;
        }

        public void displayRessources(bool afterTest)
        {
            Console.WriteLine();
            if (!afterTest)
                Console.WriteLine("List before test:");
            else
                Console.WriteLine("List after test:");

            _sidebar.NavRessource.Click();
            var NtTable = driver.FindElement(By.ClassName("table"));
            IReadOnlyCollection<IWebElement> TableRows = NtTable.FindElements(By.Id("tableRow"));

            foreach (var row in TableRows)
            {
                var name = row.FindElement(By.Id("ressourceName")).Text;
                Console.WriteLine(name);
            }

            Console.WriteLine("Quantity:" + countRessources());
            Console.WriteLine();
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