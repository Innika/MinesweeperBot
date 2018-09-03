using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using static Minesweeper_TestBot1.Configurator;
using static Minesweeper_TestBot1.GameField;

namespace Minesweeper_TestBot1
{
    [TestClass]
    public class MinesweeperBot
    {
        [TestInitialize]
        public void SetUp()
        {
            SetUpDriver();
        }

        [TestMethod]
        public void PlayMinesweeper()
        {
            GameField.SizeX = 30;
            GameField.SizeY = 16;

            drv.Navigate().GoToUrl("http://minesweeperonline.com/");

            OpenRandomSquareOnTheBeginning();

            while (true)
            {
                bool squareOpened = false;
                List<Square> squaresToWorkWith = null;
                try
                {
                    squaresToWorkWith = GetSquaresToWorkWith();
                    foreach (var square in squaresToWorkWith)
                        squareOpened = OpenNeighbourSquaresIfNotBombs(square) || squareOpened;

                    if (!squareOpened)
                        OpenRandomSquareOnTheBeginning(); //OpenSquareIfStuck(squaresToWorkWith);
                }
                catch
                {
                }
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            drv.Dispose();
        }
    }
}
