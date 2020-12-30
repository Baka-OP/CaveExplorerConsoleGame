using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

using Cave_Explorer.Models;
using Cave_Explorer.Enums;
using Cave_Explorer.Helpers;

namespace Cave_Explorer.Graphic_Components
{
    class MapEditorUI
    {
        #region Map displaying modifiers
        /// <summary>
        /// How far from the left side should the map start to display
        /// </summary>
        private const int mapStartLeftMargin = 1;
        /// <summary>
        /// How far from the top side should the map start to display
        /// </summary>
        private const int mapStartTopMargin = 1;
        private int maxMapWidth = 40;
        private int maxMapHeight = 20;
        /// <summary>
        /// From which Top index the map should start to be displayed. Used for scrolling
        /// </summary>
        private int currentTopMargin;
        /// <summary>
        /// From which Left index the map should start to be displayed. Used for scrolling
        /// </summary>
        private int currentLeftMargin;
        #endregion
        #region Indexes and selections
        /// <summary>
        /// Current section of the editor that the cursor is focused on
        /// </summary>
        private EditorUISection currentSection;

        private ConsoleColor currentColor;
        /// <summary>
        /// Current left position in the map that the cursor is pointing to
        /// </summary>
        private int cursorLeftPosition;
        /// <summary>
        /// Current top position in the map that the cursor is pointing to
        /// </summary>
        private int cursorTopPosition;
        /// <summary>
        /// Cursor index on the panel on the right.
        /// </summary>
        private int currentCursorIndex;
        /// <summary>
        /// 0 = cursor inside the map
        /// 1 = cursor in the right panel
        /// </summary>
        private int currentTabIndex;
        #endregion

        /// <summary>
        /// Whether the Displaying method should completely refresh the map editor (used for camera scrolling, where you need to completely rewrite the map)
        /// </summary>
        private bool mapRefreshRequested;
        /// <summary>
        /// Whether the editor is waiting for shutdown.
        /// </summary>
        private bool shutDown;
        /// <summary>
        /// Whether enter has been pressed, used for making the app react differently when enter has been pressed (color selection on the right panel, etc.)
        /// </summary>
        private bool enterPressed;

        public MapEditor EditorInstance { get; private set; }
        public MapEditorUI(MapEditor editorInstance)
        {
            mapRefreshRequested = true;
            currentColor = 0;
            currentSection = EditorUISection.EditMap;
            EditorInstance = editorInstance;
        }
        public void Start()
        {
            while (true)
            {
                if (shutDown)
                    return;
                else if(currentSection == EditorUISection.EditMap)
                {
                    Console.SetWindowSize(Console.LargestWindowWidth - 4, Console.LargestWindowHeight - 4);
                    if (currentTabIndex == 0)
                        WaitForInputMapEdit();
                    else if (currentTabIndex == 1)
                        WaitForInputRightPanel();
                }
                else if (currentSection == EditorUISection.EscMenu)
                {
                    WaitForInputEscMenu();
                }
            }
        }

        //Methods for reading input
        private void WaitForInputMapEdit()
        {
            while (true)
            {
                Console.CursorVisible = false;
                DisplayRightPanel();
                DisplayEditor();
                Console.CursorVisible = true;
                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (cursorTopPosition > 0)
                            cursorTopPosition--;
                        break;
                    case ConsoleKey.DownArrow:
                        //Check if cursor isn't on the edge of the map
                        if (cursorTopPosition < EditorInstance.MapHeight - 1)
                        {
                            //Cursor isn't in the right edge of the screen (no scrolling required)
                            if (cursorTopPosition < currentTopMargin + maxMapHeight)
                                cursorTopPosition++;
                            else if (cursorLeftPosition < EditorInstance.MapHeight - 1)
                            {
                                cursorTopPosition++;
                                currentTopMargin++;
                                mapRefreshRequested = true;
                            }
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (cursorLeftPosition > currentLeftMargin)
                            cursorLeftPosition--;
                        else if (currentLeftMargin > 0)
                        {
                            currentLeftMargin--;
                            cursorLeftPosition--;
                            mapRefreshRequested = true;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        //Check if cursor isn't on the edge of the map
                        if (cursorLeftPosition < EditorInstance.MapWidth - 1)
                        {
                            //Cursor isn't in the right edge of the screen (no scrolling required)
                            if (cursorLeftPosition < currentLeftMargin + maxMapWidth)
                                cursorLeftPosition++;
                            else if (cursorLeftPosition < EditorInstance.MapWidth - 1)
                            {
                                cursorLeftPosition++;
                                currentLeftMargin++;
                                mapRefreshRequested = true;
                            }
                        }
                        break;
                    case ConsoleKey.Tab:
                        currentTabIndex = 1;
                        return;
                    case ConsoleKey.Escape:
                        currentSection = EditorUISection.EscMenu;
                        Console.Clear();
                        return;
                    case ConsoleKey.Home:
                        mapRefreshRequested = true;
                        break;
                    case ConsoleKey.Backspace:
                        if (currentTabIndex == 0)
                        {
                            EditorInstance.ClearValueFromAll(cursorLeftPosition, cursorTopPosition);
                            mapRefreshRequested = true;
                        }
                        break;
                    default:
                        if (currentTabIndex == 0)
                        {
                            string inputChar = input.KeyChar.ToString().Trim();
                            if (inputChar.Length == 1)
                            {
                                EditorInstance.SetIndex(cursorLeftPosition, cursorTopPosition, inputChar[0], currentColor);
                            }
                            mapRefreshRequested = true;
                        }
                        break;
                }
            }
        }
        private void WaitForInputRightPanel()
        {
            Console.CursorVisible = false;
            while (true)
            {
                DisplayEditor();
                DisplayRightPanel();

                //Handling an enter press
                if (enterPressed)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;

                    if (currentCursorIndex == 0)
                        currentColor = SafeUserInput.OneByOneConsoleColor("Current color: ", Console.WindowWidth - 29, 8, currentColor);
                    else if (currentCursorIndex == 1)
                    {
                        if (SafeUserInput.OneByOneInt(Console.WindowWidth - 29 + 12, 10, 4, out int result))
                        {
                            EditorInstance.SetMapSize(EditorInstance.MapWidth, result);
                            mapRefreshRequested = true;
                        }
                    }
                    else if (currentCursorIndex == 2)
                    {
                        if (SafeUserInput.OneByOneInt(Console.WindowWidth - 29 + 12, 11, 4, out int result))
                        {
                            EditorInstance.SetMapSize(result, EditorInstance.MapHeight);
                            mapRefreshRequested = true;
                        }
                    }

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    enterPressed = false;

                    continue;
                }

                ConsoleKeyInfo input = Console.ReadKey(true);
                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (currentCursorIndex > 0)
                            currentCursorIndex--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentCursorIndex < 2)
                            currentCursorIndex++;
                        break;
                    case ConsoleKey.Tab:
                        currentTabIndex = 0;
                        enterPressed = false;
                        Console.CursorVisible = true;
                        return;
                    case ConsoleKey.Enter:
                        enterPressed = !enterPressed;
                        break;
                    case ConsoleKey.Escape:
                        shutDown = true;
                        return;
                }
            }
        }
        private void WaitForInputEscMenu()
        {
            bool previousVisibility = Console.CursorVisible;
            Console.CursorVisible = false;
            currentCursorIndex = 0;
            Console.SetWindowSize(35, 14);
            Console.SetBufferSize(35, 14);
            while (true)
            {
                DisplayEscMenu();

                ConsoleKey input = Console.ReadKey().Key;
                switch (input)
                {
                    case ConsoleKey.UpArrow:
                        if(currentCursorIndex > 0)
                        {
                            currentCursorIndex--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if(currentCursorIndex < 2)
                        {
                            currentCursorIndex++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (currentCursorIndex == 0)
                        {
                            currentSection = EditorUISection.EditMap;
                            Console.Clear();
                            mapRefreshRequested = true;
                            Console.CursorVisible = previousVisibility;
                            return;
                        }
                        else if (currentCursorIndex == 1)
                        {
                            shutDown = true;
                            Console.Clear();
                            return;
                        }
                        else if (currentCursorIndex == 2)
                        {
                            shutDown = true;
                            Console.Clear();
                            return;
                        }
                        break;
                }
            }
        }

        //Displaying methods
        private void DisplayEditor()
        {
            if (mapRefreshRequested)
            {
                RemoveMap();
                DisplayMap();
                mapRefreshRequested = false;
            }
            DisplayRightPanel();

            Console.SetCursorPosition(cursorLeftPosition + mapStartLeftMargin - currentLeftMargin, cursorTopPosition + mapStartTopMargin - currentTopMargin);
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
            string color = StringToColor.ConvertToString(currentColor).PadRight(12);

            MainMenuHelper.MakeFrame(Console.WindowWidth - 31, 0);
            int textStart = Console.WindowWidth - 29;
            MainMenuHelper.WriteInCenter("Settings", 3, textStart, 2);

            MainMenuHelper.WriteText("Current X position: " + cursorLeftPosition.ToString().PadRight(4), textStart, 5);
            MainMenuHelper.WriteText("Current Y position: " + (EditorInstance.MapHeight - cursorTopPosition - 1).ToString().PadRight(4), textStart, 6);

            if(currentTabIndex == 0)
            {
                MainMenuHelper.WriteText("Current color: " + color, textStart, 8);
                MainMenuHelper.WriteText("Map height: " + EditorInstance.MapHeight.ToString().PadRight(4), textStart, 10);
                MainMenuHelper.WriteText("Map width:  " + EditorInstance.MapWidth.ToString().PadRight(4), textStart, 11);
            }
            else if (currentTabIndex == 1)
            {
                //Current color
                if (currentCursorIndex == 0)
                {
                    if (!enterPressed)
                        MainMenuHelper.WriteText("Current color: " + color, textStart, 8, ConsoleColor.Gray, ConsoleColor.Blue);
                    else if (enterPressed)
                        MainMenuHelper.WriteText("Current color: " + color, textStart, 8, ConsoleColor.Black, ConsoleColor.Cyan);
                }
                else
                    MainMenuHelper.WriteText("Current color: " + color, textStart, 8);

                //Map height
                string mapHeight = EditorInstance.MapHeight.ToString().PadRight(4);
                MainMenuHelper.WriteText("Map height: ", textStart, 10);
                if (currentCursorIndex == 1)
                {
                    
                    if (!enterPressed)
                        MainMenuHelper.WriteText(mapHeight, textStart + 12, 10, ConsoleColor.Gray, ConsoleColor.Blue);
                    else if (enterPressed)
                        MainMenuHelper.WriteText(mapHeight, textStart + 12, 10, ConsoleColor.Black, ConsoleColor.Cyan);
                }
                else
                    MainMenuHelper.WriteText(mapHeight, textStart + 12, 10);

                //Map width
                string mapWidth = EditorInstance.MapWidth.ToString().PadRight(4);
                MainMenuHelper.WriteText("Map width:  ", textStart, 11);
                if (currentCursorIndex == 2)
                {
                    if (!enterPressed)
                        MainMenuHelper.WriteText(mapWidth, textStart + 12, 11, ConsoleColor.Gray, ConsoleColor.Blue);
                    else if (enterPressed)
                        MainMenuHelper.WriteText(mapWidth, textStart + 12, 11, ConsoleColor.Black, ConsoleColor.Cyan);
                }
                else
                    MainMenuHelper.WriteText(mapWidth, textStart + 12, 11);
            }
        }
        private void DisplayEscMenu()
        {
            MainMenuHelper.MakeFrame();
            MainMenuHelper.WriteInCenter("What do you want to do?", 2);

            MainMenuHelper.WriteSelectableTextInCenter("Go back", 6, 0, currentCursorIndex);
            MainMenuHelper.WriteSelectableTextInCenter("Save and exit", 8, 1, currentCursorIndex);
            MainMenuHelper.WriteSelectableTextInCenter("Exit without saving", 10, 2, currentCursorIndex);

        }

        //Helper methods
        private void RemoveMap()
        {
            for(int i = 0; i < EditorInstance.MapHeight; i++)
            {
                Console.SetCursorPosition(mapStartLeftMargin, mapStartTopMargin + i);
                Console.Write("".PadRight(maxMapWidth));
            }
        }
    }
}
