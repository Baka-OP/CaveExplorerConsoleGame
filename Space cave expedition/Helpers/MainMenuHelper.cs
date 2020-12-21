using System;
using System.Collections.Generic;
using System.Text;

namespace Space_cave_expedition.Helpers
{
    class MainMenuHelper
    {
        /// <summary>
        /// Fills one line in a console with a character, starts writing from the current line.
        /// </summary>
        /// <param name="characterToDisplay"></param>
        public static void FillALine(char characterToDisplay, int margin, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            ConsoleColor previousForeground = Console.ForegroundColor;
            ConsoleColor previousBackground = Console.BackgroundColor;

            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            Console.SetCursorPosition(margin, Console.CursorTop);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Console.WindowWidth - margin - 1; i++)
                sb.Append(characterToDisplay);
            Console.WriteLine(sb);
            Console.ForegroundColor = previousForeground;
            Console.BackgroundColor = previousBackground;
        }
        /// <summary>
        /// Fills one line in a console with a character.
        /// </summary>
        /// <param name="characterToDisplay"></param>
        public static void FillALine(char characterToDisplay, int margin, int startingTop, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            ConsoleColor previousForeground = Console.ForegroundColor;
            ConsoleColor previousBackground = Console.BackgroundColor;

            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            Console.SetCursorPosition(margin, startingTop);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Console.WindowWidth - margin - 1; i++)
                sb.Append(characterToDisplay);
            Console.WriteLine(sb);
            Console.ForegroundColor = previousForeground;
            Console.BackgroundColor = previousBackground;
        }
        /// <summary>
        /// Puts a character on the edges of the current line.
        /// </summary>
        /// <param name="characterToDisplay"></param>
        public static void MakeEdges(char characterToDisplay)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(characterToDisplay);
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.CursorTop);
            Console.WriteLine(characterToDisplay);
        }
        /// <summary>
        /// Puts a character on the edges of a specific line.
        /// </summary>
        /// <param name="characterToDisplay"></param>
        /// <param name="startingTop"></param>
        public static void MakeEdges(char characterToDisplay, int startingTop)
        {
            Console.SetCursorPosition(0, startingTop);
            Console.Write(characterToDisplay);
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.CursorTop);
            Console.WriteLine(characterToDisplay);
        }
        /// <summary>
        /// Writes text into the center of the screen.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="startingTop"></param>
        public static void WriteInCenter(string text, int startingTop, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            ConsoleColor previousForeground = Console.ForegroundColor;
            ConsoleColor previousBackground = Console.BackgroundColor;

            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            Console.SetCursorPosition(0, startingTop);
            int padding = (int)Math.Round((Console.WindowWidth / 2.0) - (text.Length / 2.0));
            Console.SetCursorPosition(padding, startingTop);
            Console.WriteLine(text);
            Console.ForegroundColor = previousForeground;
            Console.BackgroundColor = previousBackground;
        }
        /// <summary>
        /// Fills all of the edges of the main menu with = and |
        /// </summary>
        public static void MakeFrame()
        {
            //Making the edges, I removed 3 instead of 2 because I want one space on the bottom.
            FillALine('=', 1);
            for (int i = 0; i < Console.WindowHeight - 3; i++)
            {
                MakeEdges('|');
            }
            FillALine('=', 1);
        }
        /// <summary>
        /// Writes text with a specific margin (how far it is from the left side), foreground color and background color.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="margin"></param>
        /// <param name="foreground"></param>
        /// <param name="background"></param>
        public static void WriteText(string text, int margin, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            ConsoleColor previousForeground = Console.ForegroundColor;
            ConsoleColor previousBackground = Console.BackgroundColor;

            Console.SetCursorPosition(margin, Console.CursorTop);
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            Console.WriteLine(text);
            Console.ForegroundColor = previousForeground;
            Console.BackgroundColor = previousBackground;
        }
        /// <summary>
        /// Writes text with a specific margin (how far it is from the left side), foreground color and background color.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="margin"></param>
        /// <param name="foreground"></param>
        /// <param name="background"></param>
        public static void WriteText(string text, int margin, int cursorTop, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            ConsoleColor previousForeground = Console.ForegroundColor;
            ConsoleColor previousBackground = Console.BackgroundColor;

            Console.SetCursorPosition(margin, cursorTop);
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            Console.WriteLine(text);
            Console.ForegroundColor = previousForeground;
            Console.BackgroundColor = previousBackground;
        }
    }
}
