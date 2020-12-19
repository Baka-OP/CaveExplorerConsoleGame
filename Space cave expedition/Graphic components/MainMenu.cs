using System;
using System.Text;
using System.Threading;
using System.Collections.Generic;

using Space_cave_expedition.Enums;
using Space_cave_expedition.Helpers;

namespace Space_cave_expedition.Graphic_Components
{
    public class MainMenu
    {
        public static MainMenu Instance;
        private bool keepConsoleSizeMonitorerRunning;
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
            while (keepConsoleSizeMonitorerRunning)
            {
                int windowHeight = Console.WindowHeight;
                int windowWidth = Console.WindowWidth;
                Console.CursorVisible = cursorVisibility;
                Thread.Sleep(30);
                if(windowHeight != Console.WindowHeight || windowWidth != Console.WindowWidth)
                {
                    Console.Clear();
                    Thread.Sleep(400);
                    DisplayMenu();
                }
            }
        }

        /// <summary>
        /// Creates an instance of a main menu and displays it.
        /// </summary>
        public MainMenu()
        {
            keepConsoleSizeMonitorerRunning = true;
            cursorVisibility = false;
            ThreadStart ts = new ThreadStart(ConsoleSizeMonitorer);
            Thread t = new Thread(ts);
            t.Start();

            Console.SetWindowSize(60, 20);
            Console.BufferHeight = 21;
            CurrentSection = MainMenuSection.MainMenu;
            ConsoleHook.DisableAllResizingControl();
            WaitForInput();
        }
        private void WaitForInput()
        {
            while (true)
            {
                ConsoleKey pressedKey = Console.ReadKey().Key;

                switch (pressedKey)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        if (currentCursorIndex > 0)
                            currentCursorIndex--;
                        DisplayMenu();
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        if (currentCursorIndex < CursorIndexLimit)
                            currentCursorIndex++;
                        DisplayMenu();
                        break;
                    case ConsoleKey.Enter:
                        if(CurrentSection == MainMenuSection.MainMenu)
                        {
                            switch (currentCursorIndex)
                            {
                                case 0:
                                    CurrentSection = MainMenuSection.MapSelection;
                                    break;
                                case 1:
                                    CurrentSection = MainMenuSection.Editor;
                                    break;
                                case 2:
                                    CurrentSection = MainMenuSection.Settings;
                                    break;
                                case 3:
                                    keepConsoleSizeMonitorerRunning = false;
                                    Console.Clear();
                                    return;
                            }
                        }
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
            //Making the edges, I removed 3 instead of 2 because I want one space on the bottom.
            FillALine('=', 1);
            for (int i = 0; i < Console.WindowHeight - 3; i++)
            {
                MakeEdges('|');
            }
            FillALine('=', 1);
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.CursorTop);

            //Title text
            FillALine('=', 1, Console.WindowHeight / 4);
            WriteInCenter("Space cave exploration", (Console.WindowHeight / 4) / 2);

            ConsoleColor background;

            if (currentCursorIndex == 0)
                background = ConsoleColor.Blue;
            else
                background = ConsoleColor.Black;
            playPosition = 7;
            WriteInCenter("Play", playPosition, ConsoleColor.Gray, background);

            if (currentCursorIndex == 1)
                background = ConsoleColor.Blue;
            else
                background = ConsoleColor.Black;
            editorPosition = 10;
            WriteInCenter("Editor", editorPosition, ConsoleColor.Gray, background);

            if (currentCursorIndex == 2)
                background = ConsoleColor.Blue;
            else
                background = ConsoleColor.Black;
            settingsPosition = 13;
            WriteInCenter("Settings", settingsPosition, ConsoleColor.Gray, background);

            if (currentCursorIndex == 3)
                background = ConsoleColor.Blue;
            else
                background = ConsoleColor.Black;
            exitPosition = 16;
            WriteInCenter("Exit", exitPosition, ConsoleColor.Gray, background);

        }
        private void DisplayPlay()
        {
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
