using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Linq;
using static Minesweeper_TestBot1.Configurator;

namespace Minesweeper_TestBot1
{
    class Square
    {
        public int X, Y;
        public IWebElement Element;
        public Status State;
        public int Value;
        public enum Status
        {
            Open = 1,
            Closed = 2,
            BombMarked = 3,
            Border = 4
        }
        public List<IWebElement> NeighbourSquares;

        public Square(int x, int y, bool neighbourSquaresToInitiate = false)
        {
            X = x;
            Y = y;

            GetWebElement();

            SetState();
            SetValuleIfExists();

            if (neighbourSquaresToInitiate)
                NeighbourSquares = GetNeighbourSquaresElements();
        }

        public Square(IWebElement element, bool neighbourSquaresToInitiate = false)
        {
            Element = element;

            string id = element.GetAttribute("id");
            Y = int.Parse(id.Remove(id.IndexOf("_")));
            X = int.Parse(id.Replace(Y.ToString() + "_", ""));

            SetState();
            SetValuleIfExists();

            if (neighbourSquaresToInitiate)
                NeighbourSquares = GetNeighbourSquaresElements();
        }

        public IWebElement GetWebElement()
        {
            string id = string.Format("{0}_{1}", Y, X);
            Element = FindElement(By.Id(id));
            if (Element == null || !Element.Displayed)
                throw new System.Exception(string.Format("No square has coordinates {0}; {1}", Y, X));

            return Element;
        }

        public void SetState()
        {
            string className;
            try
            {
                className = Element.GetAttribute("class");
            }
            catch
            {
                className = new Square(X, Y).Element.GetAttribute("class");
            }

            State = className.Contains("open") ? Status.Open :
                className.Contains("blank") ? Status.Closed :
                className.Contains("bombflagged") ? Status.BombMarked :
                Status.Border;
        }

        public void SetValuleIfExists()
        {
            string className;
            try
            {
                className = Element.GetAttribute("class");
            }
            catch
            {
                className = new Square(X, Y).Element.GetAttribute("class");
            }

            if (State == Status.Open)
                Value = int.Parse(className.Replace("square open", ""));
        }

        public void Open()
        {
            Element.Click();
        }

        public List<IWebElement> GetNeighbourSquaresElements()
        {
            List<IWebElement> neighbourSquaresElements = new List<IWebElement>();

            string id = Element.GetAttribute("id");
            int id_y = int.Parse(id.Remove(id.IndexOf('_')));
            int id_x = int.Parse(id.Replace(id_y + "_", ""));

            for (int x = id_x - 1; x <= id_x + 1; x++)
                for (int y = id_y - 1; y <= id_y + 1; y++)
                {
                    if (x > GameField.SizeX == false && y > GameField.SizeY == false &&
                        x <= 0 == false && y <= 0 == false &&
                        (x == GameField.SizeX && y == GameField.SizeY) == false)
                        neighbourSquaresElements.Add(drv.FindElement(By.Id(string.Format("{0}_{1}", y, x))));
                }

            return neighbourSquaresElements;
        }

        public void MarkNeighbourBombs()
        {
            if (NeighbourSquares.Where(neighbourSquare => neighbourSquare.GetAttribute("class").Contains("bombflagged")
                || neighbourSquare.GetAttribute("class").Contains("blank")).Count() == Value)
            {
                foreach (var neighbourSquare in NeighbourSquares)
                    if (neighbourSquare.GetAttribute("class").Contains("blank"))
                        MarkAsBomb(neighbourSquare);
            }
        }

        internal void MarkAsBomb(IWebElement element)
        {
            if (new Square(element).State != Status.BombMarked)
            {
                Actions RightClick = new Actions(drv);
                RightClick.ContextClick(element).Perform();
            }
        }
    }
}
