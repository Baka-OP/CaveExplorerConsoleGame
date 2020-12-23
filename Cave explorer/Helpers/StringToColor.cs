using System;
using System.Collections.Generic;
using System.Text;

namespace Cave_Explorer.Helpers
{
    static class StringToColor
    {
        /// <summary>
        /// Converts from a string to a color. Returns Black if no result is found.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static ConsoleColor ConvertFromString(string input)
        {
            return (input.ToLower().Trim()) switch
            {
                "black" => ConsoleColor.Black,
                "blue" => ConsoleColor.Blue,
                "cyan" => ConsoleColor.Cyan,
                "darkblue" => ConsoleColor.DarkBlue,
                "darkcyan" => ConsoleColor.DarkCyan,
                "darkgray" => ConsoleColor.DarkGray,
                "darkgreen" => ConsoleColor.DarkGreen,
                "darkmagenta" => ConsoleColor.DarkMagenta,
                "darkred" => ConsoleColor.DarkRed,
                "darkyellow" => ConsoleColor.DarkYellow,
                "gray" => ConsoleColor.Gray,
                "green" => ConsoleColor.Green,
                "magenta" => ConsoleColor.Magenta,
                "red" => ConsoleColor.Red,
                "white" => ConsoleColor.White,
                "yellow" => ConsoleColor.Yellow,
                _ => ConsoleColor.Black,
            };
        }
    }
}
