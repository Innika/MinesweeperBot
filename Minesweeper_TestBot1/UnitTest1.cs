using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Minesweeper_TestBot1
{
    [TestClass]
    public class Minesweeper
    {
        IWebDriver drv = new ChromeDriver();
        IWebElement square(int x, int y) => drv.FindElement(By.Id(string.Format("{0}_{1}", x, y)));

        [TestMethod]
        public void PlayMinesweeper()
        {
            drv.Navigate().GoToUrl("http://minesweeperonline.com/");

            Random rnd = new Random();
            int x = rnd.Next(1, 30);
            int y = rnd.Next(1, 16);

            square(x, y).Click();

        }        
    }
}
