using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using static Minesweeper_TestBot1.Configurator;

namespace Minesweeper_TestBot1
{
    class GameField
    {
        public static int SizeX, SizeY;

        public GameField(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
        }

        public static void OpenRandomSquareOnTheBeginning()
        {
            Random rnd = new Random();
            int x = rnd.Next(1, SizeX);
            int y = rnd.Next(1, SizeY);

            var rndSquare = new Square(x, y, false);
            rndSquare.Open();
        }

        public static void OpenSquareIfStuck(List<Square> squaresToWorkWith)
        {
            //foreach(var square in squaresToWorkWith)
            //    square.NeighbourSquares.
        }

        public static bool OpenNeighbourSquaresIfNotBombs(Square square)
        {
            bool squareOpened = false;
            square.MarkNeighbourBombs();

            square = new Square(square.X, square.Y, true);
            if (square.NeighbourSquares.Where(neighbourSquare =>
            neighbourSquare.GetAttribute("class").Contains("bombflagged")).Count() == square.Value)
            {
                foreach (var neighbourSquare in square.NeighbourSquares)
                    if (neighbourSquare.GetAttribute("class").Contains("blank"))
                    {
                        neighbourSquare.Click();
                        squareOpened = true;
                    }
            }
            return squareOpened;
        }

        public static List<Square> GetSquaresToWorkWith()
        {
            System.Threading.Thread.Sleep(450);
            List<Square> squaresToWorkWith = new List<Square>();
            List<IWebElement> openSquaresElements =
                drv.FindElements(By.XPath("//div[contains(@class, 'square open')]")).ToList();

            foreach (var squareElement in openSquaresElements)
            {
                Square square = new Square(squareElement, true);

                if (square.NeighbourSquares.Any(neighbourSquare => neighbourSquare.GetAttribute("class").Contains("blank")))
                    squaresToWorkWith.Add(square);
            }

            return squaresToWorkWith;
        }
    }
}
