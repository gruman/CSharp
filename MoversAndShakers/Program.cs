using System;

namespace MoversAndShakers
{
    class Player
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Player(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    class GameSettings
    {
        public int Size { get; set; }
        public int Points {get; set;}
        public int Val { get; set; }

        public GameSettings(int size, int points, int val)
        {
            Size = size;
            Points = points;
            Val = val;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            GameSettings gameSettings = new GameSettings(6, 2, 50);
            Player enemy = new Player(gameSettings.Size-1, gameSettings.Size-1);
            Player player = new Player(0, 0);
            Random random = new Random();
            Player goal = new Player(random.Next(gameSettings.Size), random.Next(gameSettings.Size));

            Menu(gameSettings, random, player, enemy, goal);
        }

        static void Menu(GameSettings gameSettings, Random random, Player player, Player enemy, Player goal)
        {
            bool active = true;

            while (active)
            {
                GetRandomPos(player, enemy, goal, random, gameSettings);
            char[,] matrix = new char[gameSettings.Size, gameSettings.Size];
            for (int i = 0; i < gameSettings.Size; i++)
            {
                for (int j = 0; j < gameSettings.Size; j++)
                {
                    matrix[i, j] = '.';
                }
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Points: " + gameSettings.Points);
                for (int i = 0; i < gameSettings.Size; i++)
                {
                    for (int j = 0; j < gameSettings.Size; j++)
                    {
                        if (player.X == i && player.Y == j)
                        {
                            Console.Write("x ");
                        }
                        else if (enemy.X == i && enemy.Y == j)
                        {
                            Console.Write("o ");
                        }
                        else if (goal.X == i && goal.Y == j)
                        {
                            Console.Write("z ");
                        }
                        else
                        {
                            Console.Write(matrix[i, j] + " ");
                        }
                    }
                    Console.WriteLine();
                }
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        Moving(player, 0, gameSettings);
                        break;
                    case ConsoleKey.RightArrow:
                        // handle right arrow key
                        Moving(player, 1, gameSettings);
                        break;
                    case ConsoleKey.UpArrow:
                        // handle up arrow key
                        Moving(player, 2, gameSettings);
                        break;
                    case ConsoleKey.DownArrow:
                        // handle down arrow key
                        Moving(player, 3, gameSettings);
                        break;
                }
                if (player.X == goal.X && player.Y == goal.Y)
                {
                    gameSettings.Points = gameSettings.Points + gameSettings.Val;
                    gameSettings.Val = gameSettings.Val * 2;
                GetRandomPos(player, enemy, goal, random, gameSettings);
                }
                bool done = Moving(enemy, random.Next(4), gameSettings);
                while (!done)
                {
                    done = Moving(enemy, random.Next(4), gameSettings);
                }
                if (enemy.X == goal.X && enemy.Y == goal.Y)
                {
                    Console.WriteLine("Enemy wins!");
                    break;
                }
                 done = Moving(goal, random.Next(4), gameSettings);
                while (!done)
                {
                    done = Moving(goal, random.Next(4), gameSettings);
                }
            }
            }

        }

        static void GetRandomPos(Player player, Player enemy, Player goal, Random random, GameSettings gameSettings)
        {

                while ((goal.X == player.X && goal.Y == player.Y) || (goal.X == enemy.X && goal.Y == enemy.Y))
            {
                goal.X = random.Next(gameSettings.Size);
                goal.Y = random.Next(gameSettings.Size);
            }
        }

        static bool Moving(Player player, int direction, GameSettings gameSettings)
        {
            bool done = false;
            switch (direction)
            {
                case 0:
                    //left
                    if (player.Y > 0)
                    {
                        player.Y = player.Y - 1;
                        done = true;
                    }
                    break;
                case 1:
                    //right
                    if (player.Y < gameSettings.Size - 1)
                    {
                        player.Y = player.Y + 1;
                        done = true;
                    }
                    break;
                case 2:
                    //up
                    if (player.X > 0)
                    {
                        player.X = player.X - 1;
                        done = true;
                    }
                    break;
                case 3:
                    //down
                    if (player.X < gameSettings.Size - 1)
                    {
                        player.X = player.X + 1;
                        done = true;
                    }
                    break;
            }
            return done;
        }

    }
}