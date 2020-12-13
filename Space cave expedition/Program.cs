using System;
using System.IO;

using Space_cave_expedition.Models;

namespace Space_cave_expedition
{
    class Program
    {
        static string ProgramLocation = Environment.CurrentDirectory;
        static void Main(string[] args)
        {
            Console.WriteLine(new Map(File.ReadAllText(ProgramLocation + "\\Map Templates\\TestMap.txt"), ConsoleColor.Black, ConsoleColor.Gray).MapTemplate);
        }
    }
}
