using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace UITestsSelenium.WebDriver.Contact
{
    [TestClass]
    public class ContactForm
    {
        IWebDriver driver;

        [TestCleanup]
        public void TearDown()
        {
            if (driver != null)
                driver.Quit();
        }

        [TestMethod]
        public void TestOnChrome()
        {
            driver = new ChromeDriver();
            EntryTest();
        }

        [TestMethod]
        public void TestOnFirefox()
        {
            var options = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
            driver = new FirefoxDriver(options);
            EntryTest();
        }

        public void EntryTest()
        {
            driver.Manage().Window.Maximize();
            driver.Url = "https://localhost:44331/contacto/3";

            //Wait for main elements to render
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(e => e.FindElement(By.Id("contactform")));


            //Assert
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography-h4")).Text, "Seguridad y Privacidad");
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography-align-justify")).Text, "Si está interesado en este grupo no dude en contactarnos enviando un correo o por medio del formulario de \'Contáctenos\'");
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-grid-item:nth-child(1) b")).Text, "Contacto Principal");
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-grid-item-xs-6:nth-child(1)")).Text, "Nombre*");
            {
                var element = driver.FindElement(By.CssSelector(".mud-grid-item:nth-child(1) .mud-input-slot"));
                Boolean isEditable = element.Enabled && element.GetAttribute("readonly") == null;
                Assert.IsTrue(isEditable);
            }
            {
                var element = driver.FindElement(By.CssSelector(".mud-grid-item:nth-child(2) > .mud-input-control .mud-input-slot"));
                Boolean isEditable = element.Enabled && element.GetAttribute("readonly") == null;
                Assert.IsTrue(isEditable);
            }
            {
                var element = driver.FindElement(By.CssSelector(".mud-grid-item:nth-child(3) .mud-input-slot"));
                Boolean isEditable = element.Enabled && element.GetAttribute("readonly") == null;
                Assert.IsTrue(isEditable);
            }
            {
                var element = driver.FindElement(By.CssSelector(".mud-grid-item:nth-child(4) .mud-input-slot"));
                Boolean isEditable = element.Enabled && element.GetAttribute("readonly") == null;
                Assert.IsTrue(isEditable);
            }
            {
                IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".rz-html-editor-button:nth-child(1) > .rzi"));
                Assert.IsTrue(elements.Count > 0);
            }
            {
                IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".rz-html-editor-button:nth-child(2) > .rzi"));
                Assert.IsTrue(elements.Count > 0);
            }
            {
                IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".rz-html-editor-button:nth-child(3) > .rzi"));
                Assert.IsTrue(elements.Count > 0);
            }
            {
                IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".rz-html-editor-button:nth-child(4) > .rzi"));
                Assert.IsTrue(elements.Count > 0);
            }
            {
                IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".rz-html-editor-button:nth-child(5)"));
                Assert.IsTrue(elements.Count > 0);
            }
            {
                IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".rz-html-editor-button:nth-child(6) > .rzi"));
                Assert.IsTrue(elements.Count > 0);
            }
            {
                IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".rz-html-editor-color > .rzi"));
                Assert.IsTrue(elements.Count > 0);
            }
            {
                IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".rz-html-editor-dropdown-value"));
                Assert.IsTrue(elements.Count > 0);
            }
            {
                IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".rz-html-editor-button:nth-child(10) > .rzi"));
                Assert.IsTrue(elements.Count > 0);
            }
            {
                IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".rz-html-editor-button:nth-child(11) > .rzi"));
                Assert.IsTrue(elements.Count > 0);
            }
            {
                IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".rz-html-editor-button:nth-child(13) > .rzi"));
                Assert.IsTrue(elements.Count > 0);
            }
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-button-filled > .mud-button-label")).Text, "ENVIAR");
        }

    }
}
