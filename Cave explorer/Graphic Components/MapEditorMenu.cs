using System;
using System.Collections.Generic;
using System.Text;

using Cave_Explorer.Enums;
using Cave_Explorer.Models;
using Cave_Explorer.Helpers;

namespace Cave_Explorer.Graphic_Components
{
    class MapEditorMenu
    {
        string mapNameInput;

        private int currentTabIndex;
        private int currentCursorIndex;
        private int currentTabIndexLimit;
        private int currentCursorIndexLimit;
        private MapEditorSection currentSection;
        public MapEditorMenu()
        {
            mapNameInput = "";
            currentSection = MapEditorSection.Menu;
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
                        if(currentSection == MapEditorSection.Menu)
                        {
                            switch(currentCursorIndex)
                            {
                                case 0:
                                    currentSection = MapEditorSection.NewMap;
                                    Console.Clear();
                                    break;
                                case 1:
                                    currentSection = MapEditorSection.EditMap;
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
                case MapEditorSection.Menu:
                    DisplayMenu();
                    break;
                case MapEditorSection.NewMap:
                    DisplayNewMap();
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
        private void DisplayNewMap()
        {
            while (true)
            {
                currentCursorIndexLimit = 2;
                Console.SetWindowSize(30, 14);
                Console.SetBufferSize(31, 15);

                MainMenuHelper.MakeFrame();
                MainMenuHelper.WriteInCenter("New map", 2);
                MainMenuHelper.FillALine('=', 1, 4);

                MainMenuHelper.WriteInCenter("Map name: ", 6);
                MainMenuHelper.WriteText(mapNameInput.PadRight(14), 8, 8);
                MainMenuHelper.WriteInCenter("‾‾‾‾‾‾‾‾‾‾‾‾‾‾", 9);

                Console.SetCursorPosition(8 + mapNameInput.Length, 8);
                Console.CursorVisible = true;
                ConsoleKeyInfo input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.Enter:
                        StartEditor();
                        break;
                    case ConsoleKey.Backspace:
                        if (mapNameInput.Length > 0)
                            mapNameInput = mapNameInput.Remove(mapNameInput.Length - 1);
                        break;
                    case ConsoleKey.Escape:
                        currentSection = MapEditorSection.Menu;
                        Console.Clear();
                        Console.CursorVisible = false;
                        DisplayMenu(); //I need to display the menu here, or else the player will be on a blank screen.
                        return;
                    default:
                        if (mapNameInput.Length < 14)
                        {
                            if (input.KeyChar == ' ')
                                mapNameInput += ' ';
                            else
                                //trim used for getting rid of characters like pgup, del, insert, tab
                                mapNameInput += input.KeyChar.ToString().Trim(); 
                        }
                        break;
                }
            }
        }

        private void StartEditor()
        {
            Console.Clear();
            new MapEditorUI(new MapEditor(Environment.CurrentDirectory + "\\Map Layouts\\Main\\Test"));
            currentSection = MapEditorSection.Menu;
            Console.Clear();
            DisplaySection();
        }
    }
}
