using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Minesweeper_TestBot1
{
    static class Configurator
    {
        public static IWebDriver drv;

        public static void SetUpDriver()
        {
            drv = new ChromeDriver();
            drv.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            drv.Manage().Window.Maximize();
        }

        public static IWebElement FindElement(By locator)
        {
            return drv.FindElement(locator);
        }
    }
}
