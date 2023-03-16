using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace UITestsSelenium.WebDriver.ResearchCenter
{
    [TestClass]
    public class ResearchCenter
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
            driver.Url = "https://localhost:44331/inicio";

            //Wait for main elements to render
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(e => e.FindElement(By.Id("center")));

            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography-h4")).Text, "Centro de Investigaciones en Tecnologías de la Información y Comunicación");
            Assert.AreEqual(driver.Title, "Sistema Grupos Investigación ＠UCR");
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography-align-justify")).Text, "El Consejo Universitario creó en junio del 2011 el Centro de Investigaciones en Tecnologías de la Información y Comunicación (CITIC), en su sesión ordinaria N.º 5550. La creación del CITIC se dio con el objetivo de: Producir conocimiento en campos relacionados con computación e informática mediante la promoción, coordinación y desarrollo de la investigación científica inter y transdisciplinaria, y su integración con la acción social y con la docencia en grado y posgrado; la formación y consolidación de grupos de investigación y fungir como observatorio de este campo en el ámbito nacional e internacional. La Vicerrectoría de Investigación, en el oficio VI-3040-2011, del 17 de mayo de 2011, manifiesta que está de acuerdo con la creación del CITIC, y que este reúne los requisitos para ser centro y para ello tomó los siguientes rubros de evaluación.");
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-alert:nth-child(1) .mud-alert-message")).Text, "Si presiona el botón Grupos de Investigación ingresará al buscador de los grupos de investigación de este centro.");
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-alert:nth-child(2) .mud-alert-message")).Text, "El botón Estadisticas mostrará las estadísticas más relevantes de este centro de investigación.");
            {
                IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".mud-avatar-img"));
                Assert.IsTrue(elements.Count > 0);
            }
            Assert.AreEqual(driver.FindElement(By.CssSelector(".mud-typography:nth-child(4)")).Text, "Sistema Grupos Investigación ＠UCR");
            {
                IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector("a > img"));
                Assert.IsTrue(elements.Count > 0);
            }
            Assert.AreEqual(driver.FindElement(By.LinkText("Registrarse")).Text, "Registrarse");
            Assert.AreEqual(driver.FindElement(By.LinkText("Iniciar Sesión")).Text, "Iniciar Sesión");
        }
    }
}
