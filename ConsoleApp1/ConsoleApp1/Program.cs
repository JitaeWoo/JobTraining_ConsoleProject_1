namespace ConsoleApp1
{
    internal class Program
    {
        struct Position
        {
            public int x;
            public int y;
        }

        static char[,] map = new char[8, 8]
        {
            {'#', '#', '#', '#', '#', '#', '#', '#' },
            {'#', '0', '0', '0', '$', '#', '$', '#' },
            {'#', '0', '0', '0', '0', '#', '0', '#' },
            {'#', '#', '#', '#', '0', '#', '0', '#' },
            {'#', '0', '$', '#', '0', '0', '0', '#' },
            {'#', '0', '#', '#', '0', '#', '#', '#' },
            {'#', '0', '0', '0', '0', 'D', 'G', '#' },
            {'#', '#', '#', '#', '#', '#', '#', '#' },
        };

        static int point = 0;

        static void Main(string[] args)
        {
            bool gameClear = false;
            Position playerPos;

            Start(out playerPos);
            while (!gameClear)
            {
                Render(playerPos);
                ConsoleKey key = Input();
                Update(key, ref playerPos, ref gameClear);
            }
            End();
        }

        static void Start(out Position playerPos)
        {
            Console.CursorVisible = false;

            playerPos.x = 1;
            playerPos.y = 1;
        }

        static void Render(Position playerPos)
        {
            Console.SetCursorPosition(0, 0);
            PrintMap(point);
            PrintPlayer(playerPos);

        }

        static void PrintMap(int point)
        {
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    switch (map[y, x])
                    {
                        case '0':
                            Console.Write("  ");
                            break;
                        case '#':
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write("■");
                            Console.ResetColor();
                            break;
                        case '$':
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("★");
                            Console.ResetColor();
                            break;
                        case 'G':
                            Console.Write("♥");
                            break;
                        case 'D':
                            if (point < 3)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("■");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.Write("  ");
                            }
                            break;
                    }
                }
                Console.WriteLine();
            }
        }

        static void PrintPlayer(Position playerPos)
        {
            Console.SetCursorPosition(playerPos.x * 2, playerPos.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write('●');
            Console.ResetColor();
        }

        static ConsoleKey Input()
        {
            return Console.ReadKey(true).Key;
        }

        static void Update(ConsoleKey key, ref Position playerPos, ref bool gameClear)
        {
            Position nextPos = playerPos;

            switch (key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    nextPos.y--;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    nextPos.x--;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    nextPos.y++;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    nextPos.x++;
                    break;
            }

            if (!IsWall(nextPos.x, nextPos.y))
            {
                playerPos = nextPos;
            }

            if (map[playerPos.y, playerPos.x] == '$')
            {
                point++;
                map[playerPos.y, playerPos.x] = '0';
            }
            else if (map[playerPos.y, playerPos.x] == 'G')
            {
                Render(playerPos);
                gameClear = true;
            }
        }

        static bool IsWall(int x, int y)
        {
            if (map[y, x] == '#')
            {
                return true;
            }
            else if (map[y, x] == 'D' && point < 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        static void End()
        {
            Console.SetCursorPosition(0, 8);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Game Clear!!!!!!");
            Console.ResetColor();
        }
    }
}
