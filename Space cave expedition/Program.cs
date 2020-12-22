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
        }
    }
}
