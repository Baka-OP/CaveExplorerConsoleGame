using System;
using System.Collections.Generic;
using System.Text;

namespace Cave_Explorer.Helpers
{
    public static class SafeUserInput
    {
        /// <summary>
        /// Gets user input using Console.ReadKey, the user stops inputting by pressing Esc or Enter. The position in which the user input happens is on the current cursor position.
        /// </summary>
        /// <param name="inputWidth">The maximum width of the input the user can type (inputWidth = 5 means the user can input 5 characters.</param>
        /// <param name="result">Result of the input after the user is done with the input.</param>
        /// <returns>Whether the conversion succeeded.</returns>
        /// <remarks>If the conversion fails, result will be 0.</remarks>
        public static bool OneByOneInt(int inputWidth, out int result) => OneByOneInt(Console.CursorLeft, Console.CursorTop, inputWidth, out result);
        /// <summary>
        /// Gets user input using Console.ReadKey, the user stops inputting by pressing Esc or Enter.
        /// </summary>
        /// <param name="startingLeft">CursorLeft position, from which the user starts typing.</param>
        /// <param name="startingTop">CursorTop position, from which the user starts typing.</param>
        /// <param name="inputWidth">The maximum width of the input the user can type (inputWidth = 5 means the user can input 5 characters.</param>
        /// <param name="result">Result of the input after the user is done with the input.</param>
        /// <returns>Whether the conversion succeeded.</returns>
        /// <remarks>If the conversion fails, result will be 0.</remarks>
        public static bool OneByOneInt(int startingLeft, int startingTop, int inputWidth, out int result)
        {
            bool previousVisibility = Console.CursorVisible;
            StringBuilder inputText = new StringBuilder();

            Console.CursorVisible = true;
            while (true)
            {
                Console.SetCursorPosition(startingLeft, startingTop);
                Console.Write(inputText.ToString().PadRight(inputWidth));
                Console.SetCursorPosition(startingLeft + inputText.Length, startingTop);

                ConsoleKeyInfo inputKey = Console.ReadKey(true);
                switch (inputKey.Key)
                {
                    //Stop input
                    case ConsoleKey.Escape:
                    case ConsoleKey.Enter:
                        Console.CursorVisible = previousVisibility;
                        return int.TryParse(inputText.ToString(), out result);
                    //Remove a letter
                    case ConsoleKey.Backspace:
                        if(inputText.Length > 0)
                            inputText.Remove(inputText.Length - 1, 1);
                        break;
                    default:
                        string inputtedCharacter = inputKey.KeyChar.ToString().Trim();
                        if (inputtedCharacter.Length == 1)
                            inputText.Append(inputtedCharacter);
                        break;
                }
            }
        }
        /// <summary>
        /// User sets ConsoleColor using the up and down arrows. By pressing enter, the user confirms his input.
        /// Requires the width of at least 12 (if text is ""), else the color name might not fit.
        /// </summary>
        /// <param name="startingLeft">ConsoleLeft value on which the current selected color will be displayed</param>
        /// <param name="startingTop">ConsoleTop value on which the current selected color will be displayed</param>
        /// <param name="currentColor"></param>
        /// <returns></returns>
        public static ConsoleColor OneByOneConsoleColor(string text, int startingLeft, int startingTop, ConsoleColor currentColor)
        {
            bool previousVisibility = Console.CursorVisible;
            ConsoleColor newColor = currentColor;
            while (true)
            {
                Console.SetCursorPosition(startingLeft, startingTop);
                Console.WriteLine(text + StringToColor.ConvertToString(newColor).PadRight(12));

                ConsoleKey input = Console.ReadKey(true).Key;
                switch (input)
                {
                    case ConsoleKey.UpArrow:
                        if (newColor != ConsoleColor.White)
                            newColor++;
                        break;
                    case ConsoleKey.DownArrow:
                        if (newColor != ConsoleColor.Black)
                            newColor--;
                        break;
                    case ConsoleKey.Escape:
                        Console.CursorVisible = previousVisibility;
                        Console.SetCursorPosition(startingLeft, startingTop);
                        Console.WriteLine(text + StringToColor.ConvertToString(newColor).PadRight(11));
                        return currentColor;
                    case ConsoleKey.Enter:
                        Console.CursorVisible = previousVisibility;
                        return newColor;
                }
            }
        }
    }
}
