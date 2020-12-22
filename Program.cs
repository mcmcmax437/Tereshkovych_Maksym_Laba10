using System;
using System.Threading;


namespace PingPong
{
    class Program
    {
        //--------------------------------------------------------//

        // Тип искуственный интелект, подробнее в методе AI_Move
        static Random randomGenerator = new Random();
        //Player Platform - начальные данные
        static int PlayerSize = 5;
        static int PlayerPosit = 0;
        static int PlayerRes = 0;
        //AI Platform - начальные данные
        static int AIPlatformSize = 5;
        static int AIPlatformPosit = 0;
        static int AIRes = 0;
        //Ball - начальные данные 
        static int bX = 0;
        static int bY = 0;
        static bool toPlayer = true;
        static bool toAI = false;
     

        //--------------------------------------------------------//



        //--------------------------------------Методы-----------------------------------------//

        //---------------------------StartPosit------------------//
        static void StartPositionForEvrth()
        {
            PlayerPosit = Console.WindowHeight / 2 - PlayerSize / 2;
            AIPlatformPosit = Console.WindowHeight / 2 - AIPlatformSize / 2;
            BallMid();
        }
        static void Start()
        {
          
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
        }

        //---------------------Otrisovka elementov---------------//

        static void DrawAt(int x, int y, char symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

        //-----------------------------AI------------------------//

        static void AI_Platform()
        {
            for (int y = AIPlatformPosit; y < AIPlatformPosit + AIPlatformSize; y++)
            {
                DrawAt(Console.WindowWidth - 1, y, '|');
               // DrawAt(Console.WindowWidth - 2, y, '|');
            }
        }
        static void AI_Move()
        {
            int randomNumber = randomGenerator.Next(1, 101);// если уменьшить значение максимального рандомного числа, 
            if (randomNumber <= 100)                         //то и сложность игры (логика движения платформы противника) будет выше
            {                                                //так же можно увеличить значение с 50 к примеру до 90
                if (toPlayer == true)                        //сейчас я поставил 100, тестил возможно ли проиграть
                {                                       // кстати интересный баг (фича) - ставим в ифе 5 - и игра тоде будет длинной) 
                    if (AIPlatformPosit > 0)
                    {
                        AIPlatformPosit--;
                    }
                }
                else
                {
                    if (AIPlatformPosit < Console.WindowHeight - AIPlatformSize)
                    {
                        AIPlatformPosit++;
                    }
                }
            }
        }

        //-----------------------------Player--------------------//

        static void PlayerPlatform()
        {
            for (int y = PlayerPosit; y < PlayerPosit + PlayerSize; y++)
            {
                DrawAt(0, y, '|');
            }
        }
        static void MoveUp()
        {
            if (PlayerPosit > 0)
            {
                PlayerPosit--;
            }
        }
        static void MoveDown()
        {
            if (PlayerPosit < Console.WindowHeight - PlayerSize)
            {
                PlayerPosit++;
            }
        }

        //-------------------------End--------------------------//
        static void GoalResult()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 1, 0);
            Console.Write("{0}/{1}", PlayerRes, AIRes);
        }

        //---------------------------For Ball--------------------//

        static void BallMid()
        {
            bX = Console.WindowWidth / 2;
            bY = Console.WindowHeight / 2;
        }
        static void Ball()
        {
            DrawAt(bX, bY, '0');
        }
        private static void MoveBall()        //для меня этот методы был наверное самым сожным после исткуственного интелекта платформы
        {
            if (toPlayer)
            {
                bY--;
            }
            else
            {
                bY++;
            }

            if (bY == 0)
            {
                toPlayer = false;
            }
           else if (bY == Console.WindowHeight - 1)
            {
                toPlayer = true;
            }
           else if (bX == Console.WindowWidth - 1)
            {
                BallMid();
                toAI = false;
                toPlayer = true;
                PlayerRes++;
                Console.SetCursorPosition(Console.WindowWidth / 2 - 10 , Console.WindowHeight / 2 - 10);
                Console.WriteLine("!!!YOU WIN!!!");
                Console.ReadKey();
            }
           else if (bX == 0)
            {
                BallMid();
                toAI = true;
                toPlayer = true;
                AIRes++;
                Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 - 10);
                Console.WriteLine("NOT TODAY((((");
                Console.ReadKey();
            }

            if (bX < 2)
            {
                if (bY >= PlayerPosit
                    && bY < PlayerPosit + PlayerSize)
                {
                    toAI = true;
                }
            }

            if (bX >= Console.WindowWidth - 2)
            {
                if (bY >= AIPlatformPosit
                    && bY < AIPlatformPosit + AIPlatformSize)
                {
                    toAI = false;
                }
            }

            if (toAI)
            {
                bX++;
            }
            else
            {
                bX--;
            }
        }

        //----------------------------MainCode----------------------------//
        static void Main(string[] args)
        {
         
            Start();
            StartPositionForEvrth();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        MoveDown();
                    }
                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        MoveUp();
                    }
                }
                AI_Move();                //сначала движение
                MoveBall();
                Console.Clear();           //очистка консоли
                PlayerPlatform();          //потом начало отрисовки 
                AI_Platform();
                Ball();
                GoalResult();
                Thread.Sleep(60);
            }
        }
    }
}

