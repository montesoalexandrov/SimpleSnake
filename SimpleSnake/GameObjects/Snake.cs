using SimpleSnake.GameObjects.Foods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleSnake.GameObjects
{
    public class Snake
    {
        private const char snakeSymbol = '\u25CF';

        private Queue<Point> snakeElements;
        private Food[] foods;
        private Wall wall;
        private int nextLeftX;
        private int nextTopY;
        private int foodIndex;

        public Snake(Wall wall)
        {
            this.foods = new Food[3];
            this.foodIndex = this.RandomFoodNumber;
            this.snakeElements = new Queue<Point>();
            this.GetFoods();
            this.CreateSnake();
            this.wall = wall;
        }

        private void CreateSnake()
        {
            for (int leftX = 1; leftX <= 6; leftX++)
            {
                this.snakeElements.Enqueue(new Point(leftX, 2));
            }

            this.foods[this.foodIndex].SetRandomFood(snakeElements);
        }

        public int RandomFoodNumber => new Random().Next(0, this.foods.Length);

        public bool IsMoving(Point direction)
        {
            Point currentSnakeHead = this.snakeElements.Last();

            this.GetNextPoint(direction, currentSnakeHead);

            bool isPointOfSnake = this.snakeElements
                .Any(x => x.LeftX == this.nextLeftX && x.TopY == nextTopY);

            if (isPointOfSnake)
            {
                return false;
            }

            Point snakeNewHead = new Point(this.nextLeftX, this.nextTopY);

            if (snakeNewHead.LeftX == -1)
            {
                snakeNewHead.LeftX = Console.WindowWidth - 1;
            }
            else if (snakeNewHead.LeftX == Console.WindowWidth)
            {
                snakeNewHead.LeftX = 0;
            }

            if (snakeNewHead.TopY == 1)
            {
                snakeNewHead.TopY = Console.WindowHeight - 1;
            }
            else if (snakeNewHead.TopY == Console.WindowHeight)
            {
                snakeNewHead.TopY = 2;
            }

            this.snakeElements.Enqueue(snakeNewHead);
            Console.BackgroundColor = ConsoleColor.Gray;
            snakeNewHead.Draw(snakeSymbol);
            Console.BackgroundColor = ConsoleColor.White;

            if (this.foods[this.foodIndex].IsFoodPoint(snakeNewHead))
            {
                this.Eat(direction, currentSnakeHead);
            }

            Point snakeTail = this.snakeElements.Dequeue();

            snakeTail.Draw(' ');

            return true;
        }

        private void Eat(Point direction, Point currentSnakeHead)
        {
            int length = this.foods[this.foodIndex].FoodPoints;

            for (int i = 0; i < length; i++)
            {
                this.snakeElements.Enqueue(new Point(this.nextLeftX, this.nextTopY));
                this.GetNextPoint(direction, currentSnakeHead);
            }

            this.foodIndex = this.RandomFoodNumber;
            this.foods[foodIndex].SetRandomFood(snakeElements);
            wall.AddPoints(this.snakeElements);
        }

        private void GetFoods()
        {
            this.foods[0] = new FoodHash();
            this.foods[1] = new FoodDollar();
            this.foods[2] = new FoodAsterisk();
        }

        private void GetNextPoint(Point direction, Point snakeHead)
        {
            this.nextLeftX = snakeHead.LeftX + direction.LeftX;
            this.nextTopY = snakeHead.TopY + direction.TopY;
        }
    }
}