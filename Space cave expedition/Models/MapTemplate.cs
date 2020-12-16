using System;
using System.Collections.Generic;
using System.Text;

using Space_cave_expedition.Helpers;

namespace Space_cave_expedition.Models
{
    /// <summary>
    /// A base layout of the map combined with a color with which it should be displayed.
    /// </summary>
    class MapTemplate
    {
        public ConsoleColor Color { get; private set; }
        public string TextFileTemplate { get; private set; }
        public char[,] Template { get; private set; }
        public MapTemplate(string textFileTemplate, ConsoleColor color)
        {
            string[] lines = textFileTemplate.Split('\n');
            MapHeight = lines.Length;
            MapWidth = Helper.GetLongestStringLength(lines);

            Template = new char[MapWidth, MapHeight];
            for (int lineNumber = lines.Length; lineNumber > 0; lineNumber--)
            {
                for (int charNumber = 0; charNumber < lines[lineNumber].Length; charNumber++)
                {
                    Template[charNumber, lines.Length - lineNumber] = lines[lineNumber][charNumber];
                }
            }
        }

        public int MapHeight { get; private set; }
        public int MapWidth { get; private set; }
    }
}
