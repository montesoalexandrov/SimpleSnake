using System;
using System.Collections.Generic;

namespace SimpleSnake.GameObjects.Foods
{
    public abstract class Food : Point
    {
        private char foodSymbol;
        private Random random;

        public Food(char foodSymbol, int points)
        {
            this.foodSymbol = foodSymbol;
            this.FoodPoints = points;
            this.random = new Random();
        }

        public int FoodPoints { get; private set; }

        public bool IsFoodPoint(Point snake)
        {
            return this.LeftX == snake.LeftX && this.TopY == snake.TopY;
        }

        public void SetRandomFood(Queue<Point> snakeElements)
        {
            Point food = this.GetRandomPosition(snakeElements);
            food = this.GetRandomPosition(snakeElements);
            food.Draw(foodSymbol);
        }
    }
}