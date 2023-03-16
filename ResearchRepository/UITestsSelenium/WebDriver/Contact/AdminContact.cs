using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace UITestSelenium.WebDriver.Contacts
{
    [TestClass]
    public class AdminContact
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
        }
        private void PruebaIngreso()
        {
            ///Arrange
            /// Pone la pantalla en full screen
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://localhost:44331/signin");
            //Blazor is  asynchronous so, we need to wait until the page loads
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            wait.Until(e => e.FindElement(By.Id("login")));

            driver.FindElement(By.CssSelector(".mud-grid-item:nth-child(2) .mud-input-slot")).Click();
            driver.FindElement(By.CssSelector(".mud-grid-item:nth-child(2) .mud-input-slot")).SendKeys("deanvargasde@gmail.com");
            driver.FindElement(By.CssSelector(".mud-input-root-adorned-end")).Click();
            driver.FindElement(By.CssSelector(".mud-input-root-adorned-end")).SendKeys("Password123@");
            driver.FindElement(By.CssSelector(".mud-button-filled > .mud-button-label")).Click();

            Task.Delay(5000).Wait();

            driver.Navigate().GoToUrl("https://localhost:44331/admin/1/6");
            Task.Delay(3000).Wait();
            
            driver.Navigate().GoToUrl("https://localhost:44331/admin/1/6");

            Task.Delay(3000).Wait();


            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-tab-active")).Text, "CONTACTOS");
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-button-filled > .mud-button-label")).Text, "AGREGAR CONTACTOS");
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography-h6")).Text, "No se encuentran contactos");
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography-h5")).Text, "Administración");
            driver.FindElement(By.CssSelector(".mud-button-filled > .mud-button-label")).Click();
            {
                var element = driver.FindElement(By.CssSelector(".mud-button-filled > .mud-button-label"));
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).Perform();
            }
            {
                var element = driver.FindElement(By.TagName("body"));

            }
            driver.FindElement(By.CssSelector(".mud-grid-item > .mud-input-control:nth-child(1) .mud-input-slot")).Click();
            {
                var element = driver.FindElement(By.CssSelector(".mud-grid-item > .mud-input-control:nth-child(1) .mud-input-slot"));
                Boolean isEditable = element.Enabled && element.GetAttribute("readonly") == null;
                Assert.IsTrue(isEditable);
            }
            driver.FindElement(By.CssSelector(".mud-input-control:nth-child(2) .mud-input-slot")).Click();
            {
                var element = driver.FindElement(By.CssSelector(".mud-input-control:nth-child(2) .mud-input-slot"));
                Boolean isEditable = element.Enabled && element.GetAttribute("readonly") == null;
                Assert.IsTrue(isEditable);
            }
            {
                var element = driver.FindElement(By.CssSelector(".mud-grid-item:nth-child(3) .mud-button-root"));

            }
            {
                var element = driver.FindElement(By.CssSelector(".mud-input-control:nth-child(3) .mud-input-slot"));

            }
            {
                var element = driver.FindElement(By.CssSelector(".mud-grid-item-xs-12:nth-child(1)"));

            }
            driver.FindElement(By.CssSelector(".mud-grid-item-xs-12:nth-child(1)")).Click();
            driver.FindElement(By.CssSelector(".mud-input-control:nth-child(3) .mud-input-slot")).Click();
            {
                var element = driver.FindElement(By.CssSelector(".mud-input-control:nth-child(3) .mud-input-slot"));
                Boolean isEditable = element.Enabled && element.GetAttribute("readonly") == null;
                Assert.IsTrue(isEditable);
            }
            driver.FindElement(By.CssSelector(".mud-grid-item:nth-child(4)")).Click();
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-button-filled-error > .mud-button-label")).Text, "CERRAR SIN CREAR");
            driver.FindElement(By.CssSelector(".mud-button-filled-error > .mud-button-label")).Click();
        }
    }

 }

        
