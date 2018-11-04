using System;
using System.Collections.Generic;

namespace SimpleSnake.GameObjects
{
    public class Wall : Point
    {
        private const char wallSymbol = '\u25A0';

        private int playerPoints;

        public Wall(int leftX, int topY)
            : base(leftX, topY)
        {
            this.InitializeHorizontalTopBorder();
            this.PlayerInfo();
        }

        private void InitializeHorizontalTopBorder()
        {
            this.SetHorizontalLine(1);
        }

        private void SetHorizontalLine(int topY)
        {
            for (int leftX = 0; leftX < Console.WindowWidth; leftX++)
            {
                this.Draw(leftX, topY, wallSymbol);
            }
        }

        public void AddPoints(Queue<Point> snakeElements)
        {
            this.playerPoints = snakeElements.Count - 6;
            PlayerInfo();
        }

        public void PlayerInfo()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 30, 0);
            Console.Write($"PLAYER POINTS: {this.playerPoints}");
            Console.SetCursorPosition(Console.WindowWidth / 2 + 10, 0);
            Console.Write($"PLAYER LEVEL: {this.playerPoints / 10}");
        }
    }
}