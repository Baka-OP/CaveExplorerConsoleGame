using System;
using System.Collections.Generic;
using System.Text;

using Cave_Explorer.Helpers;

namespace Cave_Explorer.Models
{
    /// <summary>
    /// A base layout of the map combined with a color with which it should be displayed.
    /// </summary>
    public class MapTemplate
    {
        public ConsoleColor Color { get; private set; }
        public string TextFileTemplate { get; private set; }
        public char[,] Template { get; private set; }
        public MapTemplate(string textFileTemplate, ConsoleColor color)
        {
            string[] lines = textFileTemplate.Split('\n');
            MapHeight = lines.Length;
            MapWidth = Helper.GetLongestStringLength(lines);
            Color = color;

            Template = new char[MapWidth, MapHeight];
            for(int lineNum = 0; lineNum < lines.Length; lineNum++)
            {
                for (int charNum = 0; charNum < lines[lineNum].Length; charNum++)
                {
                    Template[charNum, lineNum] = lines[lineNum][charNum];
                }
            }
        }

        public int MapHeight { get; private set; }
        public int MapWidth { get; private set; }
    }
}
