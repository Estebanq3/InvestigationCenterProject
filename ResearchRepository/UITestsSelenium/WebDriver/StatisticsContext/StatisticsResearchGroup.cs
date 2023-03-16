using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace UITestsSelenium.WebDriver.PublicationContext
{
    [TestClass]
    public class StatisticsResearchGroup
    {
        IWebDriver driver;

        [TestCleanup]
        public void TearDown()
        {
            if (driver != null)
                driver.Quit();
        }

        /// <summary>
        /// Tests sorting the publications per year statistic to show those from 2018 & 2020
        /// </summary>
        /// Author: David Sánchez López [LosPollosHermanos]
        [TestMethod]
        public void CorrectSortPublicationsByYear()
        {
            ///Arrange
            ///Create a Chrome driver
            driver = new ChromeDriver();

            ///Act
            ///Go to Statistics page of Group 1
            driver.Navigate().GoToUrl("https://localhost:44331/estadisticas/1");
            driver.Manage().Window.Maximize();

            ///Declare an awaiter for our driver
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            //Wait for & find the canvas (MudPaper) where the graph lays at.
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(3) > div")));            
            IWebElement mudpaper = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(3) > div"));
            
            //Wait for & find the combo box (MudSelect) which hosts the filters.
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(3) > div > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-flex-start > div > div")));
            IWebElement mudselect = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(3) > div > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-flex-start > div > div"));

            //Wait for & find the button to toggle the graph to fullscreen (popup).
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(3) > div > button")));
            IWebElement fullscreenButton = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(3) > div > button"));
            
            //Assert
            //Check the state of the displayed statistics graph
            Assert.IsNotNull(mudpaper);
            Assert.IsNotNull(mudselect);
            Assert.IsNotNull(fullscreenButton);
        }

        /// <summary>
        /// Tests for pop up for statistic of publications per year
        /// </summary>
        /// Author: Pablo Otárola Rodríguez [LosPollosHermanos]
        [TestMethod]
        public void CorrectPopUpPublicationsByAuthor()
        {
            ///Arrange
            ///Create a Chrome driver
            driver = new ChromeDriver();

            ///Act
            ///Go to Statistics page of Group 1
            driver.Navigate().GoToUrl("https://localhost:44331/estadisticas/1");
            driver.Manage().Window.Maximize();

            ///Declare an awaiting for our driver
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            //Wait for and find the canvas (MudPaper) where the graph lays at.
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(5) > div")));
            IWebElement mudpaper = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(5) > div"));

            //Wait for and find the buttom for pop up
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(5) > div > button > span > svg")));
            IWebElement mudButton = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(5) > div > button > span > svg"));

            mudButton.Click();

            //Wait for and find the pop up for the graph
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(5) > div > div.mud-overlay > div.mud-overlay-content > div")));
            IWebElement mudpaperPopUp = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(5) > div > div.mud-overlay > div.mud-overlay-content > div"));

            //Wait for and find the button for close the pop up
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(5) > div > div.mud-overlay > div.mud-overlay-content > div > div > div.mud-grid-item.mud-grid-item-xs-12.mud-grid-item-md-12.d-flex.justify-start > button > span")));
            IWebElement mudButtonClosePopUp = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(5) > div > div.mud-overlay > div.mud-overlay-content > div > div > div.mud-grid-item.mud-grid-item-xs-12.mud-grid-item-md-12.d-flex.justify-start > button > span"));

            //Assert
            //Check the state of the displayed statistics graph
            Assert.IsNotNull(mudpaper);
            Assert.IsNotNull(mudButton);
            Assert.IsNotNull(mudpaperPopUp);
            Assert.IsNotNull(mudButtonClosePopUp);
        }

        /// <summary>
        /// Tests sorting the publications per research areas to show those from Software Engineering at Research Center level
        /// </summary>
        /// Author: Frank Alvarado Alfaro [LosPollosHermanos]
        [TestMethod]
        public void CorrectSortPublicationsByResearchArea()
        {
            ///Arrange
            ///Create a Chrome driver
            driver = new ChromeDriver();

            ///Act
            ///Go to Statistics page of Group 1
            driver.Navigate().GoToUrl("https://localhost:44331/estadisticas");
            driver.Manage().Window.Maximize();

            ///Declare an awaiter for our driver
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            //Wait for & find the canvas (MudPaper) where the graph lays at.
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(2) > div")));
            IWebElement mudpaper = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(2) > div"));
            
            //Wait for & find the combo box (MudSelect) which hosts the filters.
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(2) > div > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-flex-start > div > div")));
            IWebElement mudselect = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(2) > div > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-flex-start > div > div"));
            
            //Wait for & find the button to toggle the graph to fullscreen (popup).
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(2) > div > button")));
            IWebElement fullscreenButton = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(2) > div > button"));
            
            //Assert
            //Check the state of the displayed statistics graph
            Assert.IsNotNull(mudpaper);
            Assert.IsNotNull(mudselect);
            Assert.IsNotNull(fullscreenButton);
        }
    }
}
