using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace UITestSelenium.WebDriver.ResearchGroup
{
    [TestClass]
    public class ResearchGroups
    {
        IWebDriver driver;

        [TestCleanup]
        public void TearDown()
        {
            if (driver != null)
                driver.Quit();
        }

        [TestMethod]
        public void PruebaIngresoChrome()
        {
            ///Arrange
            /// Crea el driver de Chrome
            driver = new ChromeDriver();
            PruebaIngreso();
            LoadResearchGroups();
        }

        [TestMethod]
        public void PruebaIngresoFirefox()
        {
            ///Arrange
            /// Crea el driver de Chrome
            var options = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
            driver = new FirefoxDriver(options);
            PruebaIngreso();
            LoadResearchGroups();
        }
        private void PruebaIngreso()
        {
            ///Arrange
            /// Pone la pantalla en full screen
            driver.Manage().Window.Maximize();
            driver.Url = "https://localhost:44331/grupos";
            //Blazor is  asynchronous so, we need to wait until the page loads
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            //All our main container must have an Id and with this we know that the component has loaded completly
            wait.Until(e => e.FindElement(By.Id("groups")));

            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography:nth-child(4)")).Text, "Sistema Grupos Investigación ＠UCR");
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-button-text-secondary > .mud-button-label")).Text, "FILTRAR POR ÁREAS");
            driver.FindElement(By.CssSelector(".mud-input-slot")).Click();
            {
                var element = driver.FindElement(By.CssSelector(".mud-input-slot"));
                bool isEditable = element.Enabled && element.GetAttribute("readonly") == null;
                Assert.IsTrue(isEditable);
            }
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-card-actions .mud-button-label")).Text, "VER MÁS");
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography:nth-child(2)")).Text, "La Ingeniería de Software Empírica enfatiza el uso de estudios empíricos de todo tipo para acumular conocimiento. Los métodos utilizados incluyen experimentos, estudios de casos, encuestas y el uso de los datos disponibles.");
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography-h6")).Text, "Ingeniería de Software Empírica");
        }

        public void LoadResearchGroups()
        {
            driver.Navigate().GoToUrl("https://localhost:44331/grupos/1");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            {
                WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));
                wait.Until(driver => driver.FindElement(By.Id("title-card")).Text == "Ingeniería de Software Empírica");
            }
            Assert.AreEqual(driver.FindElement(By.Id("title-card")).Text, "Ingeniería de Software Empírica");
            driver.FindElement(By.Id("desc-card")).Click();
            driver.FindElement(By.CssSelector(".mud-paper:nth-child(2) > .mud-card-content")).Click();
            Assert.AreEqual(driver.FindElement(By.Id("desc-card")).Text, "La Ingeniería de Software Empírica enfatiza el uso de estudios empíricos de todo tipo para acumular conocimiento. Los métodos utilizados incluyen experimentos, estudios de casos, encuestas y el uso de los datos disponibles.");
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-paper:nth-child(5) .mud-grid-item > .mud-button-root > .mud-button-label")).Text, "VER TODAS LAS PERSONAS");
            driver.FindElement(By.CssSelector(".mud-paper:nth-child(6) .mud-grid-item:nth-child(2) .mud-card-content > .mud-typography:nth-child(1)")).Click();
            driver.FindElement(By.CssSelector(".mud-paper:nth-child(6) .mud-grid-item:nth-child(2) .mud-card-content > .mud-typography:nth-child(1)")).Click();
            driver.FindElement(By.CssSelector(".mud-paper:nth-child(6) .mud-grid-item:nth-child(2) .mud-card-content > .mud-typography:nth-child(1)")).Click();
            driver.FindElement(By.CssSelector(".mud-paper:nth-child(6) .mud-card-header-content")).Click();
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-paper:nth-child(6) > .mud-card-header .mud-typography-body2")).Text, "A continuación se presentan algunos de nuestros proyectos más recientes.");
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            {
                var element = driver.FindElement(By.CssSelector(".mud-grid:nth-child(2) > .mud-grid-item"));
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).ClickAndHold().Perform();
            }
            {
                var element = driver.FindElement(By.CssSelector(".mud-grid:nth-child(2) > .mud-grid-item"));
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).Perform();
            }
            {
                var element = driver.FindElement(By.CssSelector(".mud-grid:nth-child(2) > .mud-grid-item"));
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).Release().Perform();
            }
        }

    }
} 
        
