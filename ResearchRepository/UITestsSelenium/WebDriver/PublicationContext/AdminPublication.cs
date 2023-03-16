using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace UITestsSelenium.WebDriver.PublicationContext
{
    [TestClass]
    public class AdminPublication
    {
        IWebDriver driver;
        [TestCleanup]
        public void TearDown()
        {
            if (driver != null)
                driver.Quit();
        }

        /// <summary>
        /// Test for the navigation from a selected publication to PublicationDetailed
        /// </summary>
        /// Author: Elvis Badilla 
        [TestMethod]
        public void CorrectNavigationPublication()
        {

            /// Creat driver Chrome
            login();
            /// Creat driver Chrome
            driver.Navigate().GoToUrl("https://localhost:44331/admin/1/3");
            //wait until the condition is met 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-container > table > tbody > tr:nth-child(5) > td:nth-child(1) > a")));
            IWebElement input = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-container > table > tbody > tr:nth-child(5) > td:nth-child(1) > a"));
           
            driver.Navigate().GoToUrl(input.GetAttribute("href"));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div.mud-paper.mud-elevation-3.pa-5 > div > div:nth-child(2) > h4 > b")));

            //Assert
            //Check name of Publication
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div.mud-paper.mud-elevation-3.pa-5 > div > div:nth-child(2) > h4 > b")).Text, "Alzheimers disease early detection using a low cost three-dimensional densenet-121 architecture");
        }
        /// <summary>
        /// Login in Page
        /// </summary>
        /// Author: Elvis Badilla Mena
        public void login()
        {
            /// Creat driver Chrome
            driver = new ChromeDriver();

            ///full screen
            driver.Navigate().GoToUrl("https://localhost:44331/signin");
            driver.Manage().Window.Maximize();

            //wait until the condition is met 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div > div > div > form > div:nth-child(2) > div > div > div > input")));
            IWebElement input = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div > div > div > form > div:nth-child(2) > div > div > div > input"));

            input.SendKeys("gruposinvestigacionUCR@gmail.com");
            IWebElement input2 = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div > div > div > form > div:nth-child(3) > div > div > div > input"));
            input2.SendKeys("C0ntr@sena123");

            IWebElement button = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div > div > div > form > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-flex-end > div:nth-child(2) > button > span"));

            button.Click();

            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait2.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(1) > div.mud-card-header > div.mud-card-header-content > h4")));
            //Assert
            //Check that have no authors

        }

        // <summary>
        /// AdminPublication Correct Information
        /// </summary>
        /// Author: Elvis Badilla Mena
        [TestMethod]
        public void AdminPublicationLisPublication()
        {
            login();
            /// Creat driver Chrome
            driver.Navigate().GoToUrl("https://localhost:44331/admin/1/3");
            //wait until the condition is met 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-container > table > tbody > tr:nth-child(1) > td:nth-child(1) > a")));
            IWebElement input = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-container > table > tbody > tr:nth-child(1) > td:nth-child(1) > a"));
            //Assert
            //Check List element
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-container > table > tbody > tr:nth-child(1) > td:nth-child(1) > a")).Text, "Evaluating hyper-parameter tuning using random search in support vector machines for software effort estimation");
            //Check Title
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-toolbar.mud-toolbar-gutters.mud-table-toolbar > div > div.mud-grid-item.mud-grid-item-xs-12 > h6")).Text, "Publicaciones de este grupo");
            //Check table title of publication
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-container > table > thead > tr > th:nth-child(1)")).Text, "Título");
            //Check table Doi of publication
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-container > table > thead > tr > th:nth-child(2)")).Text, "DOI");
            //Check table year of publication
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-container > table > thead > tr > th:nth-child(3)")).Text, "Año");
            //Check table Journal o Conference of publication
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-container > table > thead > tr > th:nth-child(4)")).Text, "Journal o Conference");
            //Check table acction of publication
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-container > table > thead > tr > th:nth-child(5)")).Text, "Acciones");
            //Assert.AreEqual(driver.FindElement(By.CssSelector("")).Text, "");
         
        }
        // <summary>
        /// AdminPublication Correct Pagination
        /// </summary>
        /// Author: Elvis Badilla Mena
        [TestMethod]
        public void PaginationAdminPublication()
        {

            /// Creat driver Chrome
            login();
            /// Creat driver Chrome
            driver.Navigate().GoToUrl("https://localhost:44331/admin/1/3");
            //wait until the condition is met 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-pagination > div > div > ul > li:nth-child(5) > button > span > svg")));
            IWebElement input = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-pagination > div > div > ul > li:nth-child(5) > button > span > svg"));
            input.Click();

            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-container > table > tbody > tr:nth-child(1) > td:nth-child(2)")));

            //Assert
            //Check doi of Publication in page two
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-container > table > tbody > tr:nth-child(1) > td:nth-child(2)")).Text, "10.1145/3416508.3417121");
        }
        // <summary>
        /// Add publication button Correct 
        /// </summary>
        /// Author: Elvis Badilla Mena
        [TestMethod]
        public void addPublicationButton()
        {

            /// Creat driver Chrome
            login();
            /// Creat driver Chrome
            driver.Navigate().GoToUrl("https://localhost:44331/admin/1/3");
            //wait until the condition is met 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-toolbar.mud-toolbar-gutters.mud-table-toolbar > div > div.mud-grid-item.mud-grid-item-xs-8 > button:nth-child(1) > span")));
            IWebElement input = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-toolbar.mud-toolbar-gutters.mud-table-toolbar > div > div.mud-grid-item.mud-grid-item-xs-8 > button:nth-child(1) > span"));
            
            input.Click();

            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(6) > p")));

            //Assert
            //Check title summary in Publication add form
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(6) > p")).Text, "Resumen");
        }

        // <summary>
        /// loag publication by jsson button Correct 
        /// </summary>
        /// Author: Elvis Badilla Mena
        [TestMethod]
        public void LoadPublicationButton()
        {

            /// Creat driver Chrome
            login();
            /// Creat driver Chrome
            driver.Navigate().GoToUrl("https://localhost:44331/admin/1/3");
            //wait until the condition is met 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-toolbar.mud-toolbar-gutters.mud-table-toolbar > div > div.mud-grid-item.mud-grid-item-xs-8 > button:nth-child(2) > span")));
            IWebElement input = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-toolbar.mud-toolbar-gutters.mud-table-toolbar > div > div.mud-grid-item.mud-grid-item-xs-8 > button:nth-child(2) > span"));

            input.Click();

            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div:nth-child(1) > p")));

            //Assert
            //Check title in load Publication by jsson
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div:nth-child(1) > p")).Text, "Seleccione la publicación de tipo .json que desea cargar.");
        }
        // <summary>
        /// edit publication button Correct 
        /// </summary>
        /// Author: Elvis Badilla Mena
        [TestMethod]
        public void EditPublicationButton()
        {

            /// Creat driver Chrome
            login();
            /// Creat driver Chrome
            driver.Navigate().GoToUrl("https://localhost:44331/admin/1/3");
            //wait until the condition is met 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-container > table > tbody > tr:nth-child(1) > td:nth-child(5) > div > button:nth-child(1)")));
            IWebElement input = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-container > table > tbody > tr:nth-child(1) > td:nth-child(5) > div > button:nth-child(1)"));

            input.Click();

            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(6) > p")));

            //Assert
            //Check title in edit Publication 
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(6) > p")).Text, "Resumen");
        }
        // <summary>
        /// Delete publication button Correct 
        /// </summary>
        /// Author: Elvis Badilla Mena
        [TestMethod]
        public void DeletePublicationButton()
        {

            /// Creat driver Chrome
            login();
            /// Creat driver Chrome
            driver.Navigate().GoToUrl("https://localhost:44331/admin/1/3");
            //wait until the condition is met 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-container > table > tbody > tr:nth-child(1) > td:nth-child(5) > div > button:nth-child(2) > span > svg")));
            IWebElement input = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-container > table > tbody > tr:nth-child(1) > td:nth-child(5) > div > button:nth-child(2) > span > svg"));

            input.Click();

            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div:nth-child(1) > h5")));

            //Assert
            //Check title in delete Publication by jsson
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div:nth-child(1) > h5")).Text, "¿Esta seguro que desea eliminar la publicación: Evaluating hyper-parameter tuning using random search in support vector machines for software effort estimation?");
        }
        // <summary>
        /// search publication  
        /// </summary>
        /// Author: Elvis Badilla Mena
        [TestMethod]
        public void SeachNameAdminPublication()
        {

            /// Creat driver Chrome
            login();
            /// Creat driver Chrome
            driver.Navigate().GoToUrl("https://localhost:44331/admin/1/3");
            //wait until the condition is met 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-container > table > tbody > tr:nth-child(1) > td:nth-child(1)")));
            IWebElement input = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-toolbar.mud-toolbar-gutters.mud-table-toolbar > div > div.mud-grid-item.mud-grid-item-xs-4 > div > div > div > input"));

            input.SendKeys("Evaluating");

            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-container > table > tbody > tr > td:nth-child(1) > a")));

            //Assert
            //Check name Publication 
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-container > table > tbody > tr > td:nth-child(1) > a")).Text, "Evaluating hyper-parameter tuning using random search in support vector machines for software effort estimation");
        }
        // <summary>
        /// list Publication in Group 'Seguridad y Privacidad' (the group must be enabled)
        /// </summary>
        /// Author: Elvis Badilla Mena
        [TestMethod]
        public void AdminPublicationByNoPublicationInGroup()
        {

            /// Creat driver Chrome
            login();
            /// Creat driver Chrome
            driver.Navigate().GoToUrl("https://localhost:44331/admin/3/3");
            //wait until the condition is met 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-pagination > div > div > div.mud-alert-message")));
            //Assert
            //Check text
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-table-pagination > div > div > div.mud-alert-message")).Text, "No hay resultados para mostrar.");
        }
    }
}
