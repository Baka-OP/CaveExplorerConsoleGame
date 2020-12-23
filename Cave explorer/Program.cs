using System;

using Cave_Explorer.Graphic_Components;

namespace Cave_Explorer
{
    class Program
    {
        static void Main()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            new MainMenu();
        }
    }
}
