using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Threading;
namespace UITestSelenium.WebDriver.PeopleContext
{
    [TestClass]
    public class Selenium
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
            ///Act
            /// Se va a la URL de la aplicacion
            driver.Url = "https://localhost:44331/";
            ///Assert
            ///Assert.AreEqual(driver.FindElement(By.XPath("//")).Text, "ASP.NET");
        }

        [TestMethod]
        public void ProfileExternalLinksTest()
        {
            //arrange
            driver = new ChromeDriver();
            TryFacebookIconTest();
            TryGithubIconTest();
            TryLinkedInIconTest();
           
        }
        private void TryFacebookIconTest()
        {
            driver.Url = "https://localhost:44331/Perfil/CfDJ8AtT1hRXRmdJsmPYbdr_mK4_SnaKRwB9oGLAaOiHwxNN0FU-RpE01Svxj3W2Y2l-Dn85DCa5guMcy-akD-BJmgNMZQZTgL3A-r77c0ad4Zzs27s2RYRzTvf8-fpDfLKGnDsVYGlhDnKYa6jN6uqqQz4/1";
            //Thread.Sleep(5000);
            IWebElement iconLink = driver.FindElement(By.Id("facebookIcon"));

            //act
            iconLink.Click();


            //assert
            driver.Url.Contains("facebook");

        }
        private void TryGithubIconTest()
        {
            driver.Url = "https://localhost:44331/Perfil/CfDJ8AtT1hRXRmdJsmPYbdr_mK4_SnaKRwB9oGLAaOiHwxNN0FU-RpE01Svxj3W2Y2l-Dn85DCa5guMcy-akD-BJmgNMZQZTgL3A-r77c0ad4Zzs27s2RYRzTvf8-fpDfLKGnDsVYGlhDnKYa6jN6uqqQz4/1";
            Thread.Sleep(350);
            IWebElement iconLink = driver.FindElement(By.Id("githubIcon"));

            //act
            iconLink.Click();


            //assert
            driver.Url.Contains("github");
        }
        private void TryLinkedInIconTest()
        {
            driver.Url = "https://localhost:44331/Perfil/CfDJ8AtT1hRXRmdJsmPYbdr_mK4_SnaKRwB9oGLAaOiHwxNN0FU-RpE01Svxj3W2Y2l-Dn85DCa5guMcy-akD-BJmgNMZQZTgL3A-r77c0ad4Zzs27s2RYRzTvf8-fpDfLKGnDsVYGlhDnKYa6jN6uqqQz4/1";
            Thread.Sleep(350);
            IWebElement iconLink = driver.FindElement(By.Id("linkedinIcon"));

            //act
            iconLink.Click();


            //assert
            driver.Url.Contains("linkedin");

        }
    }
} 
        
