using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace UITestsSelenium.WebDriver.PublicationContext
{
    [TestClass]
    public class PublicationDetailed
    {
        IWebDriver driver;
        [TestCleanup]
        public void TearDown()
        {
            if (driver != null)
              driver.Quit();
        }

        /// <summary>
        /// tests to confirm correct information in the detail of a publication
        /// </summary>
        /// Author:PollosHermanos team
        [TestMethod]
        public void PublicationDetailedCorrect()
        {
            /// Creat driver Chrome
            driver = new ChromeDriver();
            ///full screen
            driver.Navigate().GoToUrl("https://localhost:44331/PublicationDetailed/10.1007/978-3-030-51517-1_1");
            driver.Manage().Window.Maximize();
            //wait until the condition is met 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div.mud-paper.mud-elevation-3.pa-5 > div > div:nth-child(1) > h6")));
           
            //Asset
            //check Title
            Assert.AreEqual(driver.Title, "Sistema Grupos Investigación ＠UCR");
            //check publication name
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div.mud-paper.mud-elevation-3.pa-5 > div > div:nth-child(2) > h4 > b")).Text, "Alzheimers disease early detection using a low cost three-dimensional densenet-121 architecture");
            //check publication summary
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div.mud-paper.mud-elevation-3.pa-5 > div > div:nth-child(7) > p.mud-typography.mud-typography-body2.mud-inherit-text.mud-typography-align-justify.ml-10")).Text, "The objective of this work is to detect Alzheimer’s disease using Magnetic Resonance Imaging. For this, we use a three-dimensional densenet-121 architecture. With the use of only freely available tools, we obtain good results: a deep neural network showing metrics of 87% accuracy, 87% sensitivity (micro-average), 88% specificity (micro-average), and 92% AUROC (micro-average) for the task of classifying five different classes (disease stages). The use of tools available for free means that this work can be replicated in developing countries.");
            //check publication Type
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div.mud-paper.mud-elevation-3.pa-5 > div > div:nth-child(1) > h6")).Text, "Conference");
            //check publication day
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div.mud-paper.mud-elevation-3.pa-5 > div > div:nth-child(5) > p")).Text, "Publicado: 23 junio 2020");
            //check publication refernce 1
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div.mud-paper.mud-elevation-3.pa-5 > div > div:nth-child(8) > p:nth-child(2)")).Text, "1 . Alzheimer’s Disease Neuroimaging Initiative: Study Design (2017).");

            Assert.AreEqual(driver.FindElement(By.LinkText("Registrarse")).Text, "Registrarse");
            Assert.AreEqual(driver.FindElement(By.LinkText("Iniciar Sesión")).Text, "Iniciar Sesión");
        }


        /// <summary>
        /// Test to confirm the correct display of the publication for a specific group.
        /// </summary>
        /// Author: Christian Rojas 
        [TestMethod]
        public void GroupWithPublications()
        {
            /// Creat driver Chrome
            driver = new ChromeDriver();
            ///full screen
            driver.Navigate().GoToUrl("https://localhost:44331/publicaciones/2");
            driver.Manage().Window.Maximize();
            //wait until the condition is met 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-center > div:nth-child(1) > div")));

            //Assert

            //check Title
            Assert.AreEqual(driver.Title, "Sistema Grupos Investigación ＠UCR");
    

            //check the principal information of the publications cards in the group.  
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-center > div:nth-child(1) > div > div > div.mud-grid-item.mud-grid-item-xs-10 > a")).Text, "Human aspects of ubiquitous computing: a study addressing willingness to use it and privacy issues");

            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-center > div:nth-child(1) > div > div > div.mud-grid-item.mud-grid-item-xs-10 > p:nth-child(3) > a")).Text, "10.1007/s12652-016-0438-4");

            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-center > div:nth-child(2) > div > div > div.mud-grid-item.mud-grid-item-xs-10 > a")).Text, "Learning principles in program visualizations: A systematic literature review");

            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-center > div:nth-child(2) > div > div > div.mud-grid-item.mud-grid-item-xs-10 > p:nth-child(3) > a")).Text, "10.1109/FIE.2016.7757692");
        }


        /// <summary>
        /// Test to confirm the correct display of the publication for a specific group. Sad path.
        /// </summary>
        /// Author: Christian Rojas
        [TestMethod]
        public void GroupWithoutPublications()
        {
            /// Creat driver Chrome
            driver = new ChromeDriver();
            ///full screen
            driver.Navigate().GoToUrl("https://localhost:44331/publicaciones/3");
            driver.Manage().Window.Maximize();
            //wait until the condition is met 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3)")));

            //Assert

            //check Title
            Assert.AreEqual(driver.Title, "Sistema Grupos Investigación ＠UCR");
        

            //check the alert message for a group without publications.
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div > div > div")).Text, "No hay resultados para mostrar");
        }
        /// <summary>
        /// tests to confirm search name of publication
        /// </summary>
        /// Author: Elvis Badilla
        [TestMethod]
        public void searchNameByPublication()
        {
            /// Creat driver Chrome
            driver = new ChromeDriver();
            ///full screen
            driver.Navigate().GoToUrl("https://localhost:44331/publicaciones/1");
            driver.Manage().Window.Maximize();
            //wait until the condition is met 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-center > div:nth-child(1) > div > div > div.mud-grid-item.mud-grid-item-xs-10 > a")));
            IWebElement input = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(2) > div.mud-grid-item.mud-grid-item-xs-4 > div > div > div > input"));
            input.SendKeys("Evaluating hyper");

            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-center > div > div > div > div.mud-grid-item.mud-grid-item-xs-10 > a")));
            //Asset
            //check name 
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-center > div > div > div > div.mud-grid-item.mud-grid-item-xs-10 > a")).Text, "Evaluating hyper-parameter tuning using random search in support vector machines for software effort estimation");
        }
        /// <summary>
        /// tests to incorrect search of publication
        /// </summary>
        /// Author: Elvis Badilla
        [TestMethod]
        public void searchIncorretText()
        {
            /// Creat driver Chrome
            driver = new ChromeDriver();
            ///full screen
            driver.Navigate().GoToUrl("https://localhost:44331/publicaciones/1");
            driver.Manage().Window.Maximize();
            //wait until the condition is met 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-center > div:nth-child(1) > div > div > div.mud-grid-item.mud-grid-item-xs-10 > a")));
            IWebElement input = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(2) > div.mud-grid-item.mud-grid-item-xs-4 > div > div > div > input"));
            input.SendKeys("Elvis");

            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div > div > div > div.mud-alert-message")));
            //Asset
            //check name 
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div > div > div > div.mud-alert-message")).Text, "No hay resultados para mostrar");
        }
        /// <summary>
        /// tests confirm search summary of publication
        /// </summary>
        /// Author: Elvis Badilla
        [TestMethod]
        public void searchPublicationBySummary()
        {
            /// Creat driver Chrome
            driver = new ChromeDriver();
            ///full screen
            driver.Navigate().GoToUrl("https://localhost:44331/publicaciones/1");
            driver.Manage().Window.Maximize();
            //wait until the condition is met 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-center > div:nth-child(1) > div > div > div.mud-grid-item.mud-grid-item-xs-10 > a")));
            IWebElement input = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(2) > div.mud-grid-item.mud-grid-item-xs-4 > div > div > div > input"));
            input.SendKeys("Studies in software effort");

            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-center > div > div > div > div.mud-grid-item.mud-grid-item-xs-10 > a")));
            //Asset
            //check name 
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-center > div > div > div > div.mud-grid-item.mud-grid-item-xs-10 > a")).Text, "Evaluating hyper-parameter tuning using random search in support vector machines for software effort estimation");
        }

        /// <summary>
        /// Test for the navigation from a selected publication to an author's profile of the chosen publication. Happy path:)
        /// </summary>
        /// Author: Diana Luna
        [TestMethod]
        public void CorrectNavigationPublicationToAuthor()
        {

            /// Creat driver Chrome
            driver = new ChromeDriver();

            ///full screen
            driver.Navigate().GoToUrl("https://localhost:44331/publicaciones/1");
            driver.Manage().Window.Maximize();

            //wait until the condition is met 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-center > div:nth-child(1) > div > div > div.mud-grid-item.mud-grid-item-xs-10 > div > div > a:nth-child(3)")));
            IWebElement input = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-center > div:nth-child(1) > div > div > div.mud-grid-item.mud-grid-item-xs-10 > div > div > a:nth-child(3)"));

            driver.Navigate().GoToUrl(input.GetAttribute("href"));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div > div.mud-container.mud-container-maxwidth-xl > div > div > div > div > div.mud-grid-item.mud-grid-item-xs-7 > h5")));

            //Assert
            //Check name of author 
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div > div.mud-container.mud-container-maxwidth-xl > div > div > div > div > div.mud-grid-item.mud-grid-item-xs-7 > h5")).Text, "Dra. Alexandra Martínez Porras");
        }

        /// <summary>
        /// Test for the navigation from a selected publication to an author's profile of the chosen publication. Sad path:(
        /// </summary>
        /// Author: Diana Luna
        [TestMethod]
        public void IncorrectNavigationPublicationToAuthor()
        {

            /// Creat driver Chrome
            driver = new ChromeDriver();

            ///full screen
            driver.Navigate().GoToUrl("https://localhost:44331/publicaciones/1");
            driver.Manage().Window.Maximize();

            //wait until the condition is met 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-center > div:nth-child(10) > div")));
            IWebElement input = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-center > div:nth-child(10) > div > div > div.mud-grid-item.mud-grid-item-xs-10"));

            //driver.Navigate().GoToUrl(input.GetAttribute("href"));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-center > div:nth-child(10) > div > div > div.mud-grid-item.mud-grid-item-xs-10 > div")));

            //Assert
            //Check that have no authors
            Assert.AreEqual(driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div:nth-child(3) > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-center > div:nth-child(10) > div > div > div.mud-grid-item.mud-grid-item-xs-10 > div")).Text, "No hay autores asociados a esta publicacion.");
        }
    }
}
