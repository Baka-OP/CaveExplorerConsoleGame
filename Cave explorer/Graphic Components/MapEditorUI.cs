using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

using Cave_Explorer.Models;
using Cave_Explorer.Enums;
using Cave_Explorer.Helpers;

namespace Cave_Explorer.Graphic_Components
{
    //I bet this code is gonna become a mess after I finish it
    class MapEditorUI
    {
        private EditorUISection currentSection;
        #region Constants
        /// <summary>
        /// How far from the left side should the map start to display
        /// </summary>
        private const int mapStartLeftMargin = 1;
        /// <summary>
        /// How far from the top side should the map start to display
        /// </summary>
        private const int mapStartTopMargin = 1;

        #endregion
        private int maxMapWidth = 40;
        private int maxMapHeight = 20;
        #region Map editor indexes and margins
        /// <summary>
        /// From which Left index the map should start to be displayed. Used for scrolling
        /// </summary>
        private int currentLeftMargin;
        /// <summary>
        /// From which Top index the map should start to be displayed. Used for scrolling
        /// </summary>
        private int currentTopMargin;
        /// <summary>
        /// Current left position in the map that the cursor is pointing to
        /// </summary>
        private int cursorLeftPosition;
        /// <summary>
        /// Current top position in the map that the cursor is pointing to
        /// </summary>
        private int cursorTopPosition;
        #endregion
        /// <summary>
        /// Cursor index on the panel on the right.
        /// </summary>
        private int rightPanelCursorIndex;
        /// <summary>
        /// 0 = cursor inside the map
        /// 1 = cursor in the right panel
        /// </summary>
        private int currentTabIndex;
        /// <summary>
        /// Whether the Displaying method should completely refresh the map editor (used for camera scrolling, where you need to completely rewrite the map)
        /// </summary>
        private bool mapRefreshRequested;

        private MapEditor EditorInstance;
        public MapEditorUI(MapEditor editor)
        {
            mapRefreshRequested = true;
            Console.SetWindowSize(Console.LargestWindowWidth - 4, Console.LargestWindowHeight - 4);
            currentSection = EditorUISection.EditMap;
            EditorInstance = editor;
            DisplayRightPanel();
            WaitForInput();
        }

        private void WaitForInput()
        {
            while (true)
            {
                Display();
                ConsoleKey input = Console.ReadKey().Key;

                switch (input)
                {
                    case ConsoleKey.UpArrow:
                        if(currentTabIndex == 0)
                        {
                            if (cursorTopPosition > 0)
                                cursorTopPosition--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentTabIndex == 0)
                        {
                            if (cursorTopPosition + 1 < EditorInstance.MapHeight)
                                cursorTopPosition++;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (currentTabIndex == 0)
                        {
                            if (cursorLeftPosition > currentLeftMargin)
                                cursorLeftPosition--;
                            else if (currentLeftMargin > 0)
                            {
                                currentLeftMargin--;
                                cursorLeftPosition--;
                                mapRefreshRequested = true;
                            }
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (currentTabIndex == 0)
                        {
                            if (cursorLeftPosition < currentLeftMargin + maxMapWidth - 1)
                                cursorLeftPosition++;
                            else if (cursorLeftPosition < EditorInstance.MapWidth - 1)
                            {
                                cursorLeftPosition++;
                                currentLeftMargin++;
                                mapRefreshRequested = true;
                            }
                        }
                        break;
                }
            }
        }

        private void Display()
        {
            Console.CursorVisible = false;
            if (mapRefreshRequested)
            {
                RemoveMap();
                DisplayMap();
                mapRefreshRequested = false;
            }
            DisplayRightPanel();

            Console.SetCursorPosition(cursorLeftPosition + mapStartLeftMargin - currentLeftMargin, cursorTopPosition + mapStartTopMargin - currentTopMargin);
            Console.CursorVisible = true;
        }
        private void DisplayMap()
        {
            maxMapHeight = Console.WindowHeight - 2;
            maxMapWidth = Console.WindowWidth - 32;

            MainMenuHelper.MakeFrame();
            foreach(MapTemplate mt in EditorInstance.Templates)
            {
                Console.ForegroundColor = mt.Color;
                
                for(int i = currentTopMargin; i < currentTopMargin + maxMapHeight; i++)
                {
                    for(int j = currentLeftMargin; j < currentLeftMargin + maxMapWidth; j++)
                    {
                        if(j < mt.MapWidth && i < mt.MapHeight && mt.Layout[j, i] != ' ')
                        {
                             Console.SetCursorPosition(j + mapStartLeftMargin - currentLeftMargin, i + mapStartTopMargin - currentTopMargin);
                             Console.Write(mt.Layout[j, i]);
                        }
                    }
                }

                Console.SetCursorPosition(cursorLeftPosition + mapStartLeftMargin, cursorTopPosition + mapStartTopMargin);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        private void DisplayRightPanel()
        {
            MainMenuHelper.MakeFrame(Console.WindowWidth - 31, 0);
            int textStart = Console.WindowWidth - 29;
            MainMenuHelper.WriteInCenter("Settings", 3, textStart, 2);

            MainMenuHelper.WriteText("Current X position: " + cursorLeftPosition.ToString().PadRight(4), textStart, 5);
            MainMenuHelper.WriteText("Current Y position: " + (EditorInstance.MapHeight - cursorTopPosition - 1).ToString().PadRight(4), textStart, 6);
        }

        private void RemoveMap()
        {
            for(int i = 0; i < EditorInstance.MapHeight; i++)
            {
                Console.SetCursorPosition(mapStartLeftMargin, mapStartTopMargin + i);
                Console.Write("".PadRight(EditorInstance.MapWidth));
            }
        }
    }
}
