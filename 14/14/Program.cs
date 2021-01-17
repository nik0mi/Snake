using System;
using System.Threading;

namespace _14
{

    class Snake
    {
        public bool gameover;

        const int width = 20;
        const int height = 20;
        int x_Head = width / 2;
        int y_Head = height / 2;
        int x_fruit;
        int y_fruit;
        int[] tailX = new int[100];
        int[] tailY = new int[100];
        int nTail = 0;
        public int speed = 90;
        public int score;
        public void getFruitCoords()
        {
            Random r = new Random();
            x_fruit = r.Next(1, width - 2);
            y_fruit = r.Next(1, height - 2);
        }
        enum eDirection
        {
            STOP,
            LEFT,
            RIGHT,
            UP,
            DOWN  
        }
        eDirection dir;
        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            for (int x = 0; x < width; x++)
            {
                Console.Write("#");
            }
            Console.Write("\n");
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool isEmpty = true;
                    if (x == 0 || x == width - 1)
                    {
                        Console.Write("#");
                        isEmpty = false;
                    }                   
                    if (x == x_Head && y == y_Head && isEmpty == true) 
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("O");
                        Console.ResetColor();
                        isEmpty = false;
                    }                   
                    if (y == y_fruit && x == x_fruit && isEmpty == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("*");
                        Console.ResetColor();
                        isEmpty = false;
                    }
                    for (int k = 0; k < nTail; k++)
                    {
                        if (tailY[k] == y && tailX[k] == x)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("o");
                            Console.ResetColor();
                            isEmpty = false;
                        }
                    }
                    if (gameover == true)
                    {
                        Console.SetCursorPosition(4, 10);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Игра окончена");
                        Console.ResetColor();
                        Console.SetCursorPosition(20, 19);//////////////////////////////////////////////////////
                    }
                    if (isEmpty == true)
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write("\n");
            }
            for (int x = 0; x < width; x++)
            {
                Console.Write("#");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" Ваши очки: " + score);
            Console.ResetColor();
        }
        public void Input()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo name = Console.ReadKey(true);
                switch (name.KeyChar)
                {
                    case 'a':
                        if (dir != eDirection.RIGHT)
                        dir = eDirection.LEFT;
                        break;
                    case 'd':
                        if (dir != eDirection.LEFT)
                        dir = eDirection.RIGHT;
                        break;
                    case 's':
                        if (dir != eDirection.UP)
                        dir = eDirection.DOWN;
                        break;
                    case 'w':
                        if (dir != eDirection.DOWN)
                        dir = eDirection.UP;
                        break;
                }
            }
        }
        public void Logic()
        {
            int prevX = tailX[0];
            int prevY = tailY[0];
            int prev2X, prev2Y;
            tailX[0] = x_Head;
            tailY[0] = y_Head;
            for (int i = 1; i < nTail; i++)
            {
                prev2X = tailX[i];
                prev2Y = tailY[i];
                tailX[i] = prevX;
                tailY[i] = prevY;
                prevX = prev2X;
                prevY = prev2Y;
            }
            switch (dir)
            {
                case (eDirection.LEFT):
                    x_Head -= 1;
                    break;
                case (eDirection.RIGHT):
                    x_Head += 1;
                    break;
                case (eDirection.DOWN):
                    y_Head ++;
                    break;
                case (eDirection.UP):
                    y_Head --;
                    break;
            }
            if( x_Head < 1 || x_Head > width - 2 || y_Head < 1 || y_Head > height - 2 )
            {
                gameover = true;
            }
            if (x_Head == x_fruit && y_Head == y_fruit)
            {
                nTail++;
                score++;
                if (speed > 1)
                {
                    speed--;
                }
                getFruitCoords();
            }
            for (int i = 0; i < nTail; i++)
            {
                if (x_Head == tailX[i] && y_Head == tailY[i])
                {
                    gameover = true;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Snake snake = new Snake();
            snake.getFruitCoords();
            while (!snake.gameover)
            {
                Thread.Sleep(snake.speed);
                snake.Input();
                snake.Logic();
                snake.Draw();
            }
            snake.Draw();
            Console.ReadKey();
        }
    }
}
