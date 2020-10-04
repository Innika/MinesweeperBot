using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Minesweeper_TestBot1
{
    [TestClass]
    public class Minesweeper
    {
        IWebDriver drv = new ChromeDriver();
        IWebElement square(int x, int y) => drv.FindElement(By.Id($"{y}_{x}"));

        [TestMethod]
        public void PlayMinesweeper()
        {
            drv.Navigate().GoToUrl("http://minesweeperonline.com/");
            
            Thread.Sleep(1000);
            
            var rnd = new Random();
            var x = rnd.Next(1, 30);
            var y = rnd.Next(1, 16);
            
            square(x, y).Click();
            
            drv.Dispose();
        }
    }
}