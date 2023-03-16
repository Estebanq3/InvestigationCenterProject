using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System;

namespace UITestsSelenium.WebDriver.ResearchNews
{
    [TestClass]
    public class NewsAdmin
    {
        IWebDriver driver;

        [TestCleanup]
        public void TearDown()
        {
            if (driver != null)
                driver.Quit();
        }

        [TestMethod]
        public void AdminNewsEntryChrome()
        {
            ///Arrange 
            /// Crea el driver de Chrome 
            driver = new ChromeDriver();
            AdminNewsEntry();
        }

        [TestMethod]
        public void AdminNewsEntryFirefox()
        {
            ///Arrange 
            /// Crea el driver de FireFox 
            /// Firefox no deja que se hagan conexiones inseguras
            var op = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
            driver = new FirefoxDriver(op);

            AdminNewsEntry();
        }

        private void AdminNewsEntry()
        {
            ///Arrange 
            // Pone la pantalla en full screen 
            driver.Manage().Window.Size = new System.Drawing.Size(1382, 754);

            //Act 
            // Se va a la URL de la aplicacion 
            driver.Url = "https://localhost:44331/admin/1/5";
            //Blazor is  asynchronous so, we need to wait until the page loads
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            //All our main container must have an Id and with this we know that the component has loaded completly
            wait.Until(e => e.FindElement(By.Id("news")));


            //Assert 
            
            Assert.AreEqual(driver.FindElement(By.LinkText("RobIE++: El robot que enseñará programación a más de 68 mil niños de preescolar (TITIBOTS y TITIBOTS Colab)")).Text, "RobIE++: El robot que enseñará programación a más de 68 mil niños de preescolar (TITIBOTS y TITIBOTS Colab)");
            Assert.AreEqual(driver.FindElement(By.LinkText("Investigador del CITIC participó en la Conferencia Internacional sobre Tendencias Inteligentes en Sistemas, Seguridad y Sostenibilidad (WS4 2021)")).Text, "Investigador del CITIC participó en la Conferencia Internacional sobre Tendencias Inteligentes en Sistemas, Seguridad y Sostenibilidad (WS4 2021)");
            Assert.AreEqual(driver.FindElement(By.LinkText("Celebración del aniversario ECCI CITIC PCI")).Text, "Celebración del aniversario ECCI CITIC PCI");
            Assert.AreEqual(driver.FindElement(By.LinkText("Celebración del aniversario ECCI CITIC PCI")).Text, "Celebración del aniversario ECCI CITIC PCI");
            Assert.AreEqual(driver.FindElement(By.LinkText("El Director del CITIC participa en el taller de elaboración del Plan Nacional de Desarrollo de las Telecomunicaciones 2022-2027")).Text, "El Director del CITIC participa en el taller de elaboración del Plan Nacional de Desarrollo de las Telecomunicaciones 2022-2027");
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-pagination-item > .mud-button-text > .mud-button-label")).Text, "2");
            driver.FindElement(By.CssSelector(".mud-main-content")).Click();
            driver.FindElement(By.CssSelector(".mud-grid-item-xs-8")).Click();
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-button-filled-size-small > .mud-button-label")).Text, "CREAR NUEVA NOTICIA");
            driver.FindElement(By.CssSelector(".mud-button-filled-size-small > .mud-button-label")).Click();
            driver.FindElement(By.CssSelector(".mud-input-required .mud-input-slot")).Click();
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-input-required .mud-input-slot")).Text, " ");
            driver.FindElement(By.CssSelector(".rz-html-editor-content")).Click();
            {
                var element = driver.FindElement(By.CssSelector(".flex-column"));
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).ClickAndHold().Perform();
            }
            {
                var element = driver.FindElement(By.CssSelector(".flex-column"));
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).Perform();
            }
            {
                var element = driver.FindElement(By.CssSelector(".flex-column"));
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).Release().Perform();
            }
            driver.FindElement(By.CssSelector(".flex-column")).Click();
            driver.FindElement(By.CssSelector(".mud-grid-item-xs-7")).Click();
            driver.FindElement(By.CssSelector(".mud-input-root-adorned-start")).Click();
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-input-root-adorned-start")).Text, " ");
        }
    }
}
