using System;
using System.Collections.Generic;
using System.Threading;

enum Border
{
    MaxRight = 79,
    MaxBottom = 23
}

enum Direction
{
    Up,
    Down,
    Left,
    Right
}

class SnakeGame
{
    static bool gameOver;
    static int score;
    static int foodX;
    static int foodY;
    static int headX;
    static int headY;
    static List<int> tailX;
    static List<int> tailY;
    static Direction direction;

    static void Main()
    {
        Setup();
        while (!gameOver)
        {
            if (Console.KeyAvailable)
            {
                Input();
            }
            Draw();
            Logic();
            Thread.Sleep(100);
        }
    }

    static void Setup()
    {
        gameOver = false;
        score = 0;
        headX = 10;
        headY = 10;
        tailX = new List<int>();
        tailY = new List<int>();
        direction = Direction.Right;

        Random random = new Random();
        foodX = random.Next(1, (int)Border.MaxRight);
        foodY = random.Next(1, (int)Border.MaxBottom);

        Console.CursorVisible = false;
        Console.SetWindowSize((int)Border.MaxRight + 1, (int)Border.MaxBottom + 1);
    }

    static void Draw()
    {
        Console.Clear();

        for (int i = 0; i <= (int)Border.MaxRight; i++)
        {
            Console.SetCursorPosition(i, 0);
            Console.Write("#");
            Console.SetCursorPosition(i, (int)Border.MaxBottom);
            Console.Write("#");
        }
        for (int i = 0; i <= (int)Border.MaxBottom; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("#");
            Console.SetCursorPosition((int)Border.MaxRight, i);
            Console.Write("#");
        }

        Console.SetCursorPosition(foodX, foodY);
        Console.Write("*");

        Console.SetCursorPosition(headX, headY);
        Console.Write("O");
        for (int i = 0; i < tailX.Count; i++)
        {
            Console.SetCursorPosition(tailX[i], tailY[i]);
            Console.Write("o");
        }

        Console.SetCursorPosition(0, (int)Border.MaxBottom + 2);
        Console.Write("Score: " + score);
    }

    static void Input()
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                if (direction != Direction.Down)
                {
                    direction = Direction.Up;
                }
                break;
            case ConsoleKey.DownArrow:
                if (direction != Direction.Up)
                {
                    direction = Direction.Down;
                }
                break;
            case ConsoleKey.LeftArrow:
                if (direction != Direction.Right)
                {
                    direction = Direction.Left;
                }
                break;
            case ConsoleKey.RightArrow:
                if (direction != Direction.Left)
                {
                    direction = Direction.Right;
                }
                break;
            case ConsoleKey.Escape:
                gameOver = true;
                break;
        }
    }

    static void Logic()
    {
        int prevX = headX;
        int prevY = headY;
        int tailCount = tailX.Count;

        switch (direction)
        {
            case Direction.Up:
                headY--;
                break;
            case Direction.Down:
                headY++;
                break;
            case Direction.Left:
                headX--;
                break;
            case Direction.Right:
                headX++;
                break;
        }

        if (headX < 1 || headX >= (int)Border.MaxRight || headY < 1 || headY >= (int)Border.MaxBottom)
        {
            gameOver = true;
        }

        if (headX == foodX && headY == foodY)
        {
            score++;
            Random random = new Random();
            foodX = random.Next(1, (int)Border.MaxRight);
            foodY = random.Next(1, (int)Border.MaxBottom);
            tailX.Add(prevX);
            tailY.Add(prevY);
        }

        for (int i = 0; i < tailCount; i++)
        {
            if (headX == tailX[i] && headY == tailY[i])
            {
                gameOver = true;
            }
        }

        if (tailCount > 0)
        {
            tailX.Insert(0, prevX);
            tailY.Insert(0, prevY);
            tailX.RemoveAt(tailCount);
            tailY.RemoveAt(tailCount);
        }
    }
}
