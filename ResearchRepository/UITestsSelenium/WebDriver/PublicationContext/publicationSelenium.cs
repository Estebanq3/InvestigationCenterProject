using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UITestsSelenium.WebDriver.PublicationContext
{
    class publicationSelenium
    {
        IWebDriver driver;
        [TestMethod]
        public void ingresoPruebaDatallePublicacion()
        {
            /// Crea el driver de Chrome
            driver = new ChromeDriver();
            /// Pone la pantalla en full screen
            driver.Navigate().GoToUrl("https://localhost:44331/grupos/1");
            driver.Manage().Window.Maximize();
            //IWebElement link =driver.FindElement(By.LinkText("Grupos de Investigación"));
            //link.Click();
            //IWebElement boton = driver.FindElement(By.ClassName("mud-button-root mud-button mud-button-filled mud-button-filled-secondary mud-button-filled-size-medium mud-ripple"));
            //IWebElement boton2= driver.FindElement(RelativeBy.WithLocator(By.ClassName("mud-button-label")).Below(boton));
            //IWebElement boton = driver.FindElement(By.Name("Ver más"));
            //boton2.Click();
            ///Assert
        }
    }
}
