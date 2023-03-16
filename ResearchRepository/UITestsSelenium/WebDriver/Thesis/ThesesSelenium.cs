using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;

namespace UITestsSelenium.WebDriver.Thesis
{
    [TestClass]
    public class ThesesSelenium
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
            ///Assert
            TestListProjects();
            PruebaInfoThesis();
        }

        public void TestListProjects()
        {
            driver.Manage().Window.Maximize();
            driver.Url = "https://localhost:44331/Tesis/1";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            wait.Until(e => e.FindElement(By.ClassName("pa-5")));

            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography-body2")).Text, "Página 1 de 1 (Total de resultados: 7)");
            Assert.AreEqual(driver.FindElement(By.LinkText("Default")).Text, "Default");
            Assert.AreEqual(driver.FindElement(By.LinkText("Evaluating an automated procedure of machine learning parameter tuning for software effort estimation")).Text, "Evaluating an automated procedure of machine learning parameter tuning for software effort estimation");
            driver.Url.Contains("Trabajo final de graduación");
            driver.Url.Contains("Estudiantes:");
            driver.Url.Contains("Persona Directora:");
            driver.Url.Contains("Comité:");

            //Assert 
            Assert.AreEqual(driver.Title, "Sistema Grupos Investigación ＠UCR");
        }

        public void PruebaInfoThesis()
        {
            driver.Manage().Window.Maximize();
            driver.Url = "https://localhost:44331/tesisInfo/1";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(e => e.FindElement(By.ClassName("pa-5")));

            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography-h4")).Text, "Evaluating an automated procedure of machine learning parameter tuning for software effort estimation");
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography-h6")).Text, "Posgrado");
            driver.Url.Contains("Resumen:");
            driver.Url.Contains("Publicaciones asociadas:");
            driver.Url.Contains("Estudiantes:");
            driver.Url.Contains("Persona Directora:");
            driver.Url.Contains("Comité:");

            //Assert 
            Assert.AreEqual(driver.Title, "Sistema Grupos Investigación ＠UCR");
        }

    }
}