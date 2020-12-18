using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

using Space_cave_expedition.Models;
using Space_cave_expedition.Enums;
using Space_cave_expedition.Interfaces;
using Space_cave_expedition.Graphic_Components;

namespace Space_cave_expedition
{
    class Program
    {
        static readonly string ProgramLocation = Environment.CurrentDirectory;
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            new MainMenu();
            /*
            Player player = new Player(2, 2);
            Map testMap = new Map(ProgramLocation + "\\Map layouts\\TestMap");
            Camera c = new Camera(4, 2, testMap);
            c.FocusedEntity = player;
            testMap.AddEntity(player);
            c.DisplayMap();
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
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(0, 20);
                Console.WriteLine((player.XPosition + "," + player.YPosition).PadRight(10));
            }
            */
        }
    }
}
