using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

using Space_cave_expedition.Enums;

namespace Space_cave_expedition.Graphic_Components
{
    public class MainMenu
    {
        public static MainMenu Instance;

        #region CursorPositions
        int currentCursorIndex;
        int CursorIndexLimit
        {
            get
            {
                switch (CurrentSection)
                {
                    case MainMenuSection.MainMenu:
                        return 3;
                    default:
                        throw new ArgumentException("Unexpected menu section.");
                }
            }
        }

        //Main menu
        private int playPosition;
        private int editorPosition;
        private int settingsPosition;
        private int exitPosition;

        //New game menu


        #endregion

        private bool cursorVisibility;
        private MainMenuSection _CurrentSection;
        public MainMenuSection CurrentSection
        {
            get { return _CurrentSection; }
            set { _CurrentSection = value; DisplayMenu(); }
        }

        /// <summary>
        /// Makes sure CursorVisibility always has its desired value, be it true or false.
        /// </summary>
        private void ConsoleSizeMonitorer()
        {
            while (true)
            {
                int windowHeight = Console.WindowHeight;
                int windowWidth = Console.WindowWidth;
                Console.CursorVisible = cursorVisibility;
                Thread.Sleep(40);
                if(windowHeight != Console.WindowHeight || windowWidth != Console.WindowWidth)
                {
                    Console.Clear();
                    Thread.Sleep(400);
                    DisplayMenu();
                    continue;
                }
            }
        }

        /// <summary>
        /// Creates an instance of a main menu and displays it.
        /// </summary>
        public MainMenu()
        {
            cursorVisibility = false;
            ThreadStart ts = new ThreadStart(ConsoleSizeMonitorer);
            Thread t = new Thread(ts);
            t.Start();

            Console.SetWindowSize(60, 20);
            Console.BufferHeight = 21;
            CurrentSection = MainMenuSection.MainMenu;
            WaitForInput();
        }
        private void WaitForInput()
        {
            while (true)
            {
                ConsoleKey pressedKey = Console.ReadKey().Key;

                switch (pressedKey)
                {
                    case ConsoleKey.UpArrow:
                        if (currentCursorIndex > 0)
                            currentCursorIndex--;
                        DisplayMenu();
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentCursorIndex < CursorIndexLimit)
                            currentCursorIndex++;
                        DisplayMenu();
                        break;

                }
            }
        }

        public void DisplayMenu()
        {
            Console.SetCursorPosition(0, 0);
            switch (CurrentSection)
            {
                case MainMenuSection.MainMenu:
                    DisplayMainMenu();
                    break;
                default:
                    throw new ArgumentException("Unexpected MainMenuSection.");
            }
        }

        //Section displays
        private void DisplayMainMenu()
        {
            //Making the edges
            FillALine('=', 1);
            for (int i = 0; i < Console.WindowHeight - 2; i++)
            {
                MakeEdges('|');
            }
            FillALine('=', 1);
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.CursorTop);

            //Title text
            FillALine('=', 1, Console.WindowHeight / 4);
            WriteInCenter("Space cave exploration", (Console.WindowHeight / 4) / 2);

            //I want the positions of each to be on specific eights of the screen
            //Play = 3 eights
            //Editor = 4 eights (1 half)
            //Settings = 5 eights
            //Thus, I calculated the distance between them.
            int difference = Console.WindowHeight / 2 - (int)Math.Ceiling(Console.WindowHeight / 8 * 3.0) - 2;
            ConsoleColor background;
            Console.SetCursorPosition(0, Console.CursorTop + difference + 1);

            if (currentCursorIndex == 0)
                background = ConsoleColor.Blue;
            else
                background = ConsoleColor.Black;
            playPosition = Console.CursorTop + difference;
            WriteInCenter("Play", playPosition, ConsoleColor.Gray, background);

            if (currentCursorIndex == 1)
                background = ConsoleColor.Blue;
            else
                background = ConsoleColor.Black;
            editorPosition = Console.CursorTop + difference;
            WriteInCenter("Editor", editorPosition, ConsoleColor.Gray, background);

            if (currentCursorIndex == 2)
                background = ConsoleColor.Blue;
            else
                background = ConsoleColor.Black;
            settingsPosition = Console.CursorTop + difference;
            WriteInCenter("Settings", settingsPosition, ConsoleColor.Gray, background);

            if (currentCursorIndex == 3)
                background = ConsoleColor.Blue;
            else
                background = ConsoleColor.Black;
            exitPosition = Console.CursorTop + difference;
            WriteInCenter("Exit", exitPosition, ConsoleColor.Gray, background);
            Console.SetCursorPosition(0, 0);
        }



        //Displaying helper methods
        /// <summary>
        /// Fills one line in a console with a character, starts writing from the current line.
        /// </summary>
        /// <param name="characterToDisplay"></param>
        private void FillALine(char characterToDisplay, int margin, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
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
        private void FillALine(char characterToDisplay, int margin, int startingTop, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
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
        private void MakeEdges(char characterToDisplay)
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
        private void MakeEdges(char characterToDisplay, int startingTop)
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
        private void WriteInCenter(string text, int startingTop, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            ConsoleColor previousForeground = Console.ForegroundColor;
            ConsoleColor previousBackground = Console.BackgroundColor;

            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            Console.SetCursorPosition(0, startingTop);
            int padding = (Console.WindowWidth / 2) - (text.Length / 2);
            Console.SetCursorPosition(padding, startingTop);
            Console.WriteLine(text);
            Console.ForegroundColor = previousForeground;
            Console.BackgroundColor = previousBackground;
        }
    }
}
