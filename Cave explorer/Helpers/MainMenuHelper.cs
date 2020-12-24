using System;
using System.Collections.Generic;
using System.Text;

namespace Cave_Explorer.Helpers
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
        /// Puts a character on the edges of a specific line.
        /// </summary>
        /// <param name="characterToDisplay"></param>
        /// <param name="startingTop"></param>
        public static void MakeEdges(char characterToDisplay, int startingTop, int leftMargin, int rightMargin)
        {
            Console.SetCursorPosition(leftMargin, startingTop);
            Console.Write(characterToDisplay);
            Console.SetCursorPosition(Console.WindowWidth - rightMargin - 1, Console.CursorTop);
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
        /// Writes text in the center of a specified range, derived from the margins (leftMargin = 5 means the first five console positions will be ignored in this calculation)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="startingTop"></param>
        public static void WriteInCenter(string text, int startingTop, int leftMargin, int rightMargin, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            ConsoleColor previousForeground = Console.ForegroundColor;
            ConsoleColor previousBackground = Console.BackgroundColor;

            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;

            Console.SetCursorPosition(0, startingTop);
            int width = Console.WindowWidth - leftMargin - rightMargin;
            int padding = (int)Math.Round((width / 2.0) - (text.Length / 2.0));
            Console.SetCursorPosition(padding + leftMargin, startingTop);
            Console.WriteLine(text);
            /*
            Console.SetCursorPosition(0, startingTop);
            int padding = (int)Math.Round((Console.WindowWidth / 2.0) - (text.Length / 2.0));
            Console.SetCursorPosition(padding, startingTop);
            Console.WriteLine(text);*/
            Console.ForegroundColor = previousForeground;
            Console.BackgroundColor = previousBackground;


        }
        /// <summary>
        /// Fills all of the edges of the main menu with = and |
        /// </summary>
        /// <param name="leaveASpaceOnTheBottom">Leaves a space on the bottom so that the console doesn't automatically scroll to the bottom while making the frame, use false if the space on the bottom is too large</param>
        public static void MakeFrame(bool leaveASpaceOnTheBottom = true)
        {
            int edgeAmount;
            if (leaveASpaceOnTheBottom)
                edgeAmount = Console.WindowHeight - 3;
            else
                edgeAmount = Console.WindowHeight - 2;

            Console.SetCursorPosition(0, 0);
            FillALine('=', 1);
            for (int i = 0; i < edgeAmount; i++)
            {
                MakeEdges('|');
            }
            FillALine('=', 1);
        }
        /// <summary>
        /// Fills all of the edges of the main menu with = and | with a specific margin -> doesn't cover the edges of the console.
        /// </summary>
        /// <param name="leaveASpaceOnTheBottom">Leaves a space on the bottom so that the console doesn't automatically scroll to the bottom while making the frame, use false if the space on the bottom is too large</param>
        public static void MakeFrame(int leftMargin, int rightMargin, bool leaveASpaceOnTheBottom = true)
        {
            int edgeAmount;
            if (leaveASpaceOnTheBottom)
                edgeAmount = Console.WindowHeight - 3;
            else
                edgeAmount = Console.WindowHeight - 2;

            Console.SetCursorPosition(0, 0);
            FillALine('=', 1);
            for (int i = 0; i < edgeAmount; i++)
            {
                MakeEdges('|', Console.CursorTop, leftMargin, rightMargin);
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
