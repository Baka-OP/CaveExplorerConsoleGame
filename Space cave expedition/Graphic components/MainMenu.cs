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

        private bool cursorVisibility;
        private MainMenuSection _CurrentSection;
        public MainMenuSection CurrentSection
        {
            get { return _CurrentSection; }
            set { _CurrentSection = value; DisplayMenu(); }
        }

        /// <summary>
        /// Creates an instance of a main menu and displays it.
        /// </summary>
        public MainMenu()
        {
            cursorVisibility = false;
            ThreadStart ts = new ThreadStart(CursorVisibilitySetter);
            Thread t = new Thread(ts);
            t.Start();

            CurrentSection = MainMenuSection.MainMenu;
            Console.SetWindowSize(60, 20);
            DisplayMainMenu();
        }
        /// <summary>
        /// Makes sure CursorVisibility always has its desired value, be it true or false.
        /// </summary>
        private void CursorVisibilitySetter()
        {
            while (true)
            {
                Console.CursorVisible = cursorVisibility;
                Thread.Sleep(500);
            }
        }

        public void DisplayMenu()
        {
            switch (CurrentSection)
            {
                case MainMenuSection.MainMenu:

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
                Console.SetCursorPosition(0, Console.CursorTop + 1);
            }
            FillALine('=', 1);
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.CursorTop);

            //Title text
            int quarterOfScreen = Console.WindowHeight / 4;
            FillALine('=', 1, quarterOfScreen);
            WriteInCenter("Space cave exploration", quarterOfScreen / 2);


            //Play text
            double threeEights = Math.Ceiling((double)Console.WindowHeight / 8 * 3);
            WriteInCenter("New game", (int)threeEights);

            int difference = 

            //Editor
            WriteInCenter("Editor", Console.WindowHeight / 2);

            //Settings
            double fiveEights = Math.Ceiling((double)Console.WindowHeight / 8 * 5);
            WriteInCenter("Settings", (int)fiveEights);

            //Exit
            double threeQuarters = Math.Ceiling((double)Console.WindowHeight / 4 * 3);
            WriteInCenter("Exit", (int)threeQuarters);
        }



        //Displaying helper methods
        /// <summary>
        /// Fills one line in a console with a character, starts writing from the current line.
        /// </summary>
        /// <param name="characterToDisplay"></param>
        private void FillALine(char characterToDisplay, int margin)
        {
            Console.SetCursorPosition(margin, Console.CursorTop);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Console.WindowWidth - margin - 1; i++)
                sb.Append(characterToDisplay);
            Console.WriteLine(sb);
        }
        /// <summary>
        /// Fills one line in a console with a character.
        /// </summary>
        /// <param name="characterToDisplay"></param>
        private void FillALine(char characterToDisplay, int margin, int startingTop)
        {
            Console.SetCursorPosition(margin, startingTop);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Console.WindowWidth - margin - 1; i++)
                sb.Append(characterToDisplay);
            Console.WriteLine(sb);
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
            Console.Write(characterToDisplay);
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
            Console.Write(characterToDisplay);
        }
        /// <summary>
        /// Writes text into the center of the screen.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="startingTop"></param>
        private void WriteInCenter(string text, int startingTop)
        {
            Console.SetCursorPosition(0, startingTop);
            int padding = (Console.WindowWidth / 2) - (text.Length / 2);
            Console.SetCursorPosition(padding, startingTop);
            Console.WriteLine(text);
        }
    }
}
