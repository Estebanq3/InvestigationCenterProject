using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;

namespace ECCI_IS_Lab01_WebApp.Selenium
{
    [TestClass]
    public class InvestigationProjectsSelenium
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
            PruebaListadoProyectos();
            PruebaInfoProject();
        }

        public void PruebaListadoProyectos()
        {
            driver.Manage().Window.Maximize();
            driver.Url = "https://localhost:44331/proyectos/1";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            wait.Until(e => e.FindElement(By.Id("projects")));

            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography-h6")).Text, "Intervenciones en infancia temprana para reducir la desigualdad en las oportunidades educativas");
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography-body2")).Text, "Página 1 de 1 (Total de resultados: 5)");
            driver.Url.Contains("Proyectos de Investigación");

            //Assert 
            Assert.AreEqual(driver.Title, "Sistema Grupos Investigación ＠UCR");
        }

        public void PruebaInfoProject()
        {
            driver.Manage().Window.Maximize();
            driver.Url = "https://localhost:44331/infoProject/5";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            wait.Until(e => e.FindElement(By.ClassName("py-6")));

            //Assert 
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography-h4")).Text, "Procedimiento automatizado de medición de contribuciones a partir de repositorios de proyectos de desarrollo de software");
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography-h6")).Text, "Colaboradores:");
            Assert.AreEqual(driver.Title, "Sistema Grupos Investigación ＠UCR");
        }
    }
}