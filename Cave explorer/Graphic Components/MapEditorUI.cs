using System;
using System.Collections.Generic;
using System.Text;

using Cave_Explorer.Models;
using Cave_Explorer.Enums;

namespace Cave_Explorer.Graphic_Components
{
    //I bet this code is gonna become a mess after I finish it
    class MapEditorUI
    {
        private EditorUISection currentSection;
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
        /// <summary>
        /// Cursor index on the panel on the right.
        /// </summary>
        private int currentCursorIndex;
        /// <summary>
        /// 0 = cursor inside the map
        /// 1 = cursor in the right panel
        /// </summary>
        private int currentTabIndex;
        private MapEditor EditorInstance;
        public MapEditorUI(MapEditor editor)
        {
            Console.SetWindowSize(Console.LargestWindowWidth - 4, Console.LargestWindowHeight - 4);
            currentSection = EditorUISection.EditMap;
            EditorInstance = editor;
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
                        if(currentTabIndex == 0)
                        {
                            cursorTopPosition++;
                        }
                        break;
                }
            }
        }

        private void Display()
        {
            Console.WriteLine("Test.");
        }

        private void DisplayMap()
        {

        }
        private void DisplayRightPanel()
        {

        }
    }
}
