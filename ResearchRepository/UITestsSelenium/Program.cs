using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace UITestsSelenium
{
    class Program
    {
        static void Main(string[] args) {
            IWebDriver driver  = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44331/");
            driver.Manage().Window.Maximize();

        }
    }
}
