using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace UITestsSelenium.WebDriver.ResearchNews
{
    [TestClass]
    public class NewsList
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
            LoadNewsList();
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

            LoadNewsList();
        }

        public void LoadNewsList()
        {
            driver.Navigate().GoToUrl("https://localhost:44331/noticias/1");
            driver.Manage().Window.Size = new System.Drawing.Size(2576, 1056);
            {
                WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));
                wait.Until(driver => driver.FindElement(By.LinkText("El Dr. Mauricio Soto imparte charla \"Optimizando la calidad de parches de código creados en el proceso de reparación automática de software")).Text == "El Dr. Mauricio Soto imparte charla \"Optimizando la calidad de parches de código creados en el proceso de reparación automática de software");
            }
            {
                var element = driver.FindElement(By.CssSelector(".mud-input-slot"));
                Boolean isEditable = element.Enabled && element.GetAttribute("readonly") == null;
                Assert.IsTrue(isEditable);
            }
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-disabled")).Text, "Grupos de Investigación");
            driver.FindElement(By.CssSelector(".mud-grid > #banner")).Click();
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography-body2:nth-child(1)")).Text, "Página 1 de 4 (Total de resultados: 10)");
            {
                IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".mud-button-filled > .mud-button-label"));
                Assert.IsTrue(elements.Count > 0);
            }
        }
    }
}
