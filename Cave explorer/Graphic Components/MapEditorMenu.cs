using System;
using System.Collections.Generic;
using System.Text;

using Cave_Explorer.Enums;
using Cave_Explorer.Helpers;

namespace Cave_Explorer.Graphic_Components
{
    class MapEditorMenu
    {
        private int currentTabIndex;
        private int currentCursorIndex;
        private int currentTabIndexLimit;
        private int currentCursorIndexLimit;
        private EditorSection currentSection;
        public MapEditorMenu()
        {
            currentSection = EditorSection.Menu;
            DisplaySection();
            WaitForInput();
        }
        private void WaitForInput()
        {
            while (true)
            {
                ConsoleKey input = Console.ReadKey(true).Key;
                switch (input)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        if (currentCursorIndex > 0)
                            currentCursorIndex--;
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        if (currentCursorIndex < currentCursorIndexLimit)
                            currentCursorIndex++;
                        break;

                    case ConsoleKey.Enter:
                        if(currentSection == EditorSection.Menu)
                        {
                            switch(currentCursorIndex)
                            {
                                case 0:
                                    currentSection = EditorSection.NewMap;
                                    Console.Clear();
                                    break;
                                case 1:
                                    currentSection = EditorSection.EditMap;
                                    Console.Clear();
                                    break;
                                case 2:
                                    Console.Clear();
                                    return;
                            }
                        }
                        break;
                }
                DisplaySection();
            }
        }

        //Displaying methods
        private void DisplaySection()
        {
            switch (currentSection)
            {
                case EditorSection.Menu:
                    DisplayMenu();
                    break;
                default:
                    throw new ArgumentException("Unexpected section.");
            }
        }

        private void DisplayMenu()
        {
            currentCursorIndexLimit = 2;
            Console.SetWindowSize(30, 14);
            Console.SetBufferSize(31, 15);

            MainMenuHelper.MakeFrame();
            MainMenuHelper.WriteInCenter("Map editor", 2);
            MainMenuHelper.FillALine('=', 1, 4);

            if(currentCursorIndex == 0)
                MainMenuHelper.WriteInCenter("Create a new map", 6, ConsoleColor.Gray, ConsoleColor.Blue);
            else
                MainMenuHelper.WriteInCenter("Create a new map", 6);

            if(currentCursorIndex == 1)
                MainMenuHelper.WriteInCenter("Edit an existing map", 8, ConsoleColor.Gray, ConsoleColor.Blue);
            else
                MainMenuHelper.WriteInCenter("Edit an existing map", 8);
            
            if(currentCursorIndex == 2)
                MainMenuHelper.WriteInCenter("Back to main menu", 10, ConsoleColor.Gray, ConsoleColor.Blue);
            else
                MainMenuHelper.WriteInCenter("Back to main menu", 10);

        }
    }

}
