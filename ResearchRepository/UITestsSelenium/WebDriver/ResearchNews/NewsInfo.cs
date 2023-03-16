using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;

namespace UITestsSelenium.WebDriver.ResearchNews
{
    [TestClass]
    public class NewsInfo
    {

        IWebDriver driver;

        [TestCleanup]
        public void TearDown()
        {
            if (driver != null)
                driver.Quit();
        }

        [TestMethod]
        public void InfoNewsEntryChrome()
        {
            ///Arrange 
            /// Crea el driver de Chrome 
            driver = new ChromeDriver();
            InfoNewsEntry();
        }

        [TestMethod]
        public void InfoNewsEntryFirefox()
        {
            ///Arrange 
            /// Crea el driver de FireFox 
            /// Firefox no deja que se hagan conexiones inseguras
            var op = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
            driver = new FirefoxDriver(op);

            InfoNewsEntry();
        }

        private void InfoNewsEntry()
        {
            ///Arrange 
            // Pone la pantalla en full screen 
            driver.Manage().Window.Maximize();

            //Act 
            // Se va a la URL de la aplicacion 
            driver.Url = "https://localhost:44331/newsInfo/2";
            //Blazor is  asynchronous so, we need to wait until the page loads
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            //All our main container must have an Id and with this we know that the component has loaded completly
            wait.Until(e => e.FindElement(By.Id("news")));


            //Assert 
            Assert.AreEqual(driver.Title, "Sistema Grupos Investigación ＠UCR");
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography-h4")).Text, "RobIE++: El robot que enseñará programación a más de 68 mil niños de preescolar (TITIBOTS y TITIBOTS Colab)");
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography-subtitle2")).Text, "Fecha y hora de publicación: 23/8/2021 00:00:00");
            Assert.AreEqual(driver.FindElement(By.LinkText("Registrarse")).Text, "Registrarse");
            Assert.AreEqual(driver.FindElement(By.LinkText("Iniciar Sesión")).Text, "Iniciar Sesión");
        }
    }
}
