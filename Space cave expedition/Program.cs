using System;
using System.IO;
using System.Threading;

using Space_cave_expedition.Models;
using Space_cave_expedition.Enums;

namespace Space_cave_expedition
{
    class Program
    {
        /// <summary>
        /// If the player resizes the console, the cursor becomes visible again, this will make it invisible again every 1 second. Run this on a paralel thread, or else the whole main thread will be blocked by thread.sleep.
        /// </summary>
        static void CursorVisibilitySetter()
        {
            while (true)
            {
                Console.CursorVisible = false;
                Thread.Sleep(1000);
            }
        }

        static readonly string ProgramLocation = Environment.CurrentDirectory;
        static void Main()
        {
            ThreadStart CursorVisibilitySetterMethod = CursorVisibilitySetter;
            Thread CursorVisibilitySetterThread = new Thread(CursorVisibilitySetterMethod);
            CursorVisibilitySetterThread.Start();

            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;

            Player player = new Player(0, 2);
            Map testMap = new Map(File.ReadAllText(ProgramLocation + "\\Map Templates\\TestMap.txt"), ConsoleColor.Black, ConsoleColor.Gray, player);
            testMap.Start();

            while (true)
            {
                ConsoleKey pressedKey = Console.ReadKey(true).Key;
                switch (pressedKey)
                {
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        testMap.MoveEntity(player, EntityMoveDirection.Left);
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        testMap.MoveEntity(player, EntityMoveDirection.Right);
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        testMap.MoveEntity(player, EntityMoveDirection.Up);
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        testMap.MoveEntity(player, EntityMoveDirection.Down);
                        break;
                }
            }
        }
    }
}
