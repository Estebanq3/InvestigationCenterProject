using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System;

namespace UITestsSelenium.WebDriver.Authentication
{
    [TestClass]
    public class LoginTests
    {
        IWebDriver driver;

        [TestMethod]
        public void IncorrectLoginTest()
        {
            //arrange
            var options = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
            driver = new FirefoxDriver(options);
            driver.Url = "https://localhost:44331/signin";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            IWebElement emailEntry = wait.Until(e => e.FindElement(By.Id("EmailField")));
            IWebElement passwordEntry = wait.Until(e => e.FindElement(By.Id("PasswordField")));
            IWebElement loginButton = wait.Until(e => e.FindElement(By.Id("Login")));

            //act
            emailEntry.SendKeys("abc@gmail.com");
            passwordEntry.SendKeys("Contrasena12.");
            Thread.Sleep(350);
            loginButton.Click();

            //assert
            driver.Url.Contains("signin");

        }

        [TestMethod]
        public void InvalidEmailTest()
        {
            //arrange
            var options = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
            driver = new FirefoxDriver(options);
            driver.Url = "https://localhost:44331/signin";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            IWebElement emailEntry = wait.Until(e => e.FindElement(By.Id("EmailField")));
            IWebElement passwordEntry = wait.Until(e => e.FindElement(By.Id("PasswordField")));
            IWebElement loginButton = wait.Until(e => e.FindElement(By.Id("Login")));

            emailEntry.SendKeys("abc");
            passwordEntry.SendKeys("Contrasena12.");
            Thread.Sleep(350);

            var en = loginButton.Enabled;
            //assert
            Assert.AreEqual(en, false);
        }

        [TestMethod]
        public void EmptyPassWordTest()
        {
            //arrange
            var options = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
            driver = new FirefoxDriver(options);
            driver.Url = "https://localhost:44331/signin";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            IWebElement emailEntry = wait.Until(e => e.FindElement(By.Id("EmailField")));
            IWebElement passwordEntry = wait.Until(e => e.FindElement(By.Id("PasswordField")));
            IWebElement loginButton = wait.Until(e => e.FindElement(By.Id("Login")));

            emailEntry.SendKeys("abc@gmail.com");
            Thread.Sleep(350);

            var en = loginButton.Enabled;
            //assert
            Assert.AreEqual(en, false);
        }

        [TestMethod]
        public void EmptyEmailTest()
        {
            //arrange
            var options = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
            driver = new FirefoxDriver(options);
            driver.Url = "https://localhost:44331/signin";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            IWebElement emailEntry = wait.Until(e => e.FindElement(By.Id("EmailField")));
            IWebElement passwordEntry = wait.Until(e => e.FindElement(By.Id("PasswordField")));
            IWebElement loginButton = wait.Until(e => e.FindElement(By.Id("Login")));

            passwordEntry.SendKeys("Contrasena12.");
            Thread.Sleep(350);

            var en = loginButton.Enabled;
            //assert
            Assert.AreEqual(en, false);
        }

    }
}