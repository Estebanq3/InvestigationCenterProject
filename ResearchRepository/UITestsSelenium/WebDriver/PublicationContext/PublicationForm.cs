using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace UITestsSelenium.WebDriver.PublicationContext
{
    [TestClass]
    public class PublicationForm
    {
        IWebDriver driver;

        [TestCleanup]
        public void TearDown()
        {
            if (driver != null)
                driver.Quit();
        }

        /// <summary>
        /// Login in Page
        /// </summary>
        /// Author: Elvis Badilla Mena
        public void login()
        {
            /// Create driver Chrome
            driver = new ChromeDriver();

            ///full screen
            driver.Navigate().GoToUrl("https://localhost:44331/signin");
            driver.Manage().Window.Maximize();

            //Wait until the condition is met 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div > div > div > form > div:nth-child(2) > div > div > div > input")));
            IWebElement input = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div > div > div > form > div:nth-child(2) > div > div > div > input"));

            input.SendKeys("gruposinvestigacionUCR@gmail.com");
            IWebElement input2 = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div > div > div > form > div:nth-child(3) > div > div > div > input"));
            input2.SendKeys("C0ntr@sena123");

            IWebElement button = driver.FindElement(By.CssSelector("#banner > div > div > div > div > div > div > form > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-flex-end > div:nth-child(2) > button"));
            button.Click();

            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //wait2.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div:nth-child(1) > div.mud-card-header > div.mud-card-header-content > h4")));
            wait2.Until(e => e.FindElement(By.CssSelector("#banner > div")));
            //wait2.Until(e => e.FindElement(By.XPath("")));

        }

        /// <summary>
        /// This test verifies that all required input elements exist.
        /// </summary>
        /// Author: David Sánchez López [LosPollosHermanos]
        [TestMethod]
        public void findAllElements()
        {
            ///Act
            //First procede to login with an adquate account
            login();
            //Navigate to the AdminPublications page
            driver.Navigate().GoToUrl("https://localhost:44331/admin/1/3");

            //Await an element to be found
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-toolbar.mud-toolbar-gutters.mud-table-toolbar > div > div.mud-grid-item.mud-grid-item-xs-8 > button:nth-child(1)")));
            //Find and click the button to open the new Publication form
            IWebElement agregarButton = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-toolbar.mud-toolbar-gutters.mud-table-toolbar > div > div.mud-grid-item.mud-grid-item-xs-8 > button:nth-child(1)"));
            agregarButton.Click();

            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(2) > div > div.mud-input-control-input-container > div > input")));
            IWebElement nameTextField = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(2) > div > div.mud-input-control-input-container > div > input"));

            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(3) > div > div.mud-input-control-input-container > div > input")));
            IWebElement doiTextField = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(3) > div > div.mud-input-control-input-container > div > input"));

            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(4) > div")));
            IWebElement typeComboBox = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(4) > div"));

            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(5) > div > div.mud-input-control-input-container > div > input")));
            IWebElement publisherTextField = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(5) > div > div.mud-input-control-input-container > div > input"));

            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(6) > div > div > div > div > input")));
            IWebElement dateTextField = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(6) > div > div > div > div > input"));

            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(7) > div > div.rz-html-editor-content")));
            IWebElement summaryTextArea = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(7) > div > div.rz-html-editor-content"));

            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-space-between.pa-5 > div.mud-grid-item.mud-grid-item-xs-7 > div > div:nth-child(1) > div")));
            IWebElement authorsComboBox = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-space-between.pa-5 > div.mud-grid-item.mud-grid-item-xs-7 > div > div:nth-child(1) > div"));

            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-space-between.pa-5 > div.mud-grid-item.mud-grid-item-xs-7 > div > div:nth-child(2) > div")));
            IWebElement projectsComboBox = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-space-between.pa-5 > div.mud-grid-item.mud-grid-item-xs-7 > div > div:nth-child(2) > div"));

            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-space-between.pa-5 > div.mud-grid-item.mud-grid-item-xs-7 > div > div:nth-child(3) > div")));
            IWebElement thesesComboBox = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-space-between.pa-5 > div.mud-grid-item.mud-grid-item-xs-7 > div > div:nth-child(3) > div"));

            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-space-between.pa-5 > div.mud-grid-item.mud-grid-item-xs-5 > div > div:nth-child(2) > label > span")));
            IWebElement imageButton = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div.mud-grid.mud-grid-spacing-xs-3.mud-grid-justify-xs-space-between.pa-5 > div.mud-grid-item.mud-grid-item-xs-5 > div > div:nth-child(2) > label > span"));

            //wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(9) > button.mud-button-root.mud-button.mud-button-filled.mud-button-filled-secondary.mud-button-filled-size-small.mud-ripple")));
            //IWebElement createButton = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(9) > button.mud-button-root.mud-button.mud-button-filled.mud-button-filled-secondary.mud-button-filled-size-small.mud-ripple"));

            wait.Until(e => e.FindElement(By.CssSelector("#banner > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(10) > button.mud-button-root.mud-button.mud-button-filled.mud-button-filled-error.mud-button-filled-size-small.mud-ripple")));
            IWebElement closeButton = driver.FindElement(By.CssSelector("#banner > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(10) > button.mud-button-root.mud-button.mud-button-filled.mud-button-filled-error.mud-button-filled-size-small.mud-ripple"));

            ///Assert
            //Check title summary in Publication add form
            Assert.IsNotNull(nameTextField);
            Assert.IsNotNull(doiTextField);
            Assert.IsNotNull(typeComboBox);
            Assert.IsNotNull(publisherTextField);
            Assert.IsNotNull(dateTextField);
            Assert.IsNotNull(summaryTextArea);
            Assert.IsNotNull(authorsComboBox);
            Assert.IsNotNull(projectsComboBox);
            Assert.IsNotNull(thesesComboBox);
            Assert.IsNotNull(imageButton);
            //Assert.IsNotNull(createButton);
            Assert.IsNotNull(closeButton);
        }

        /// <summary>
        /// This test simulates a scenario where an existing DOI is written and a message is displayed.
        /// </summary>
        /// Author: David Sánchez López [LosPollosHermanos]
        [TestMethod]
        public void verifyDoiError()
        {
            ///Act
            //First procede to login with an adquate account
            login();

            //Navigate to the AdminPublications page
            driver.Navigate().GoToUrl("https://localhost:44331/admin/1/3");

            //Await an element to be found
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.CssSelector("#banner > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-toolbar.mud-toolbar-gutters.mud-table-toolbar > div > div.mud-grid-item.mud-grid-item-xs-8 > button:nth-child(1)")));
            
            //Find and click the button to open the new Publication form
            IWebElement agregarButton = driver.FindElement(By.CssSelector("#banner > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-toolbar.mud-toolbar-gutters.mud-table-toolbar > div > div.mud-grid-item.mud-grid-item-xs-8 > button:nth-child(1)"));
            agregarButton.Click();
            
            //Find the text field where the new DOI should be written
            wait.Until(e => e.FindElement(By.CssSelector("#banner > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(3) > div > div.mud-input-control-input-container > div > input")));
            IWebElement doiTextField = driver.FindElement(By.CssSelector("#banner > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(3) > div > div.mud-input-control-input-container > div > input"));
            
            //Click the text field and write a DOI (which we know already exists in the DB)
            doiTextField.Click();
            doiTextField.SendKeys("10.1007/978-3-030-40690-5_26");

            //Click outside of the text field (since the error label uses the OnBlur event)
            driver.FindElement(By.CssSelector("#banner > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div"))
                .Click();

            //Await the error label to be shown and then find it
            //wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-close-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(3) > h6")));
            //IWebElement doiErrorMessage = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-close-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(3) > h6"));
            wait.Until(e => e.FindElement(By.CssSelector("#banner > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(3) > h6")));
            IWebElement doiErrorMessage = driver.FindElement(By.CssSelector("#banner > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(3) > h6"));

            ///Assert
            //Assert.AreEqual( doiErrorMessage.Text, "Atención: Este DOI ya está registrado.");
            Assert.IsNotNull(doiErrorMessage);
        }

        /// <summary>
        /// This test simulates a scenario where an unsecure name is written and an error message is displayed.
        /// </summary>
        /// Author: David Sánchez López [LosPollosHermanos]
        [TestMethod]
        public void verifyNameSecurityError()
        {
            ///Act
            //First procede to login with an adquate account
            login();

            //Navigate to the AdminPublications page
            driver.Navigate().GoToUrl("https://localhost:44331/admin/1/3");

            //Await an element to be found
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.CssSelector("#banner > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-toolbar.mud-toolbar-gutters.mud-table-toolbar > div > div.mud-grid-item.mud-grid-item-xs-8 > button:nth-child(1)")));

            //Find and click the button to open the new Publication form
            IWebElement agregarButton = driver.FindElement(By.CssSelector("#banner > div > div > div > div.mud-tabs-panels.pa-6 > div > div.mud-toolbar.mud-toolbar-gutters.mud-table-toolbar > div > div.mud-grid-item.mud-grid-item-xs-8 > button:nth-child(1)"));
            agregarButton.Click();

            //Find the text field where the new DOI should be written
            wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(3) > div > div.mud-input-control-input-container > div > input")));
            IWebElement nameTextField = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-open-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(3) > div > div.mud-input-control-input-container > div > input"));
            
            //Click the text field and write a DOI (which we know already exists in the DB)
            nameTextField.Click();
            nameTextField.SendKeys("This is a test name which could exploit' an SQL injection...");

            //Click outside of the text field (since the error label uses the OnBlur event)
            driver.FindElement(By.CssSelector("#banner > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div"))
                .Click();

            //Await the error label to be shown and then find it
            //wait.Until(e => e.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-close-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(3) > h6")));
            //IWebElement nameErrorMessage = driver.FindElement(By.CssSelector("body > div.mud-layout.mud-drawer-close-responsive-md-left.mud-drawer-left-clipped-never > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(3) > h6"));
            wait.Until(e => e.FindElement(By.CssSelector("#banner > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(2) > h6")));
            IWebElement nameErrorMessage = driver.FindElement(By.CssSelector("#banner > div > div > div > div.mud-tabs-panels.pa-6 > div.mud-overlay > div.mud-overlay-content > div > div > div > div:nth-child(2) > h6"));

            ///Assert
            //Assert.AreEqual(nameErrorMessage.Text, "Atención: No se permite el caracter ' .");
            Assert.IsNotNull(nameErrorMessage);
        }

    }
}
