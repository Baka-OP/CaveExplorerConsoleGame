using System;
using System.IO;

using Space_cave_expedition.Models;
using Space_cave_expedition.Enums;

namespace Space_cave_expedition
{
    class Program
    {
        static string ProgramLocation = Environment.CurrentDirectory;
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            Player player = new Player(0, 15);

            Map testMap = new Map(File.ReadAllText(ProgramLocation + "\\Map Templates\\TestMap.txt"), ConsoleColor.Black, ConsoleColor.Gray, player);
            testMap.Start();

            while (true)
            {
                ConsoleKey pressedKey = Console.ReadKey(true).Key;
                switch (pressedKey)
                {
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        player.Move(PlayerMoveDirection.Left);
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        player.Move(PlayerMoveDirection.Right);
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        player.Move(PlayerMoveDirection.Up);
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        player.Move(PlayerMoveDirection.Down);
                        break;
                }
            }
        }
    }
}
