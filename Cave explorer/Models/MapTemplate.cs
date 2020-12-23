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
        /// <summary>
        /// Returns a path to the txt file this template uses, if this template doesn't use a txt file, null is returned
        /// </summary>
        public string TextFileTemplatePath { get; private set; }
        /// <summary>
        /// Color the template is supposed to have when displaying.
        /// </summary>
        public ConsoleColor Color { get; set; }
        private string _TextFileTemplate;
        /// <summary>
        /// Txt file content before parsing.
        /// </summary>
        public string TextFileTemplate
        {
            get
            {
                if(_TextFileTemplate == null)
                {
                    StringBuilder result = new StringBuilder();
                    for(int i = 0; i < MapHeight; i++)
                    {
                        for(int j = 0; j < MapWidth; j++)
                        {
                            result.Append(Layout[j, i]);
                        }
                    }
                    return result.ToString();
                }
                else
                {
                    return _TextFileTemplate;
                }
            }
            set
            {
                _TextFileTemplate = value;
            }
        }
        /// <summary>
        /// Layout of the map.
        /// </summary>
        public char[,] Layout { get; private set; }
        /// <summary>
        /// Creates a template from a file.
        /// </summary>
        /// <param name="textFileTemplate"></param>
        /// <param name="color"></param>
        public MapTemplate(string textFileTemplatePath, ConsoleColor color)
        {
            TextFileTemplatePath = textFileTemplatePath;

            TextFileTemplate = System.IO.File.ReadAllText(textFileTemplatePath);
            string[] lines = TextFileTemplate.Split('\n');
            MapHeight = lines.Length;
            MapWidth = Helper.GetLongestStringLength(lines);
            Color = color;

            Layout = new char[MapWidth, MapHeight];
            for(int lineNum = 0; lineNum < lines.Length; lineNum++)
            {
                for (int charNum = 0; charNum < lines[lineNum].Length; charNum++)
                {
                    Layout[charNum, lineNum] = lines[lineNum][charNum];
                }
            }
        }
        /// <summary>
        /// Creates a new MapTemplate
        /// </summary>
        /// <param name="mapWidth"></param>
        /// <param name="mapHeight"></param>
        /// <param name="color"></param>
        public MapTemplate(int mapWidth, int mapHeight, ConsoleColor color = ConsoleColor.Gray)
        {
            Layout = new char[mapWidth, mapHeight];
            Color = color;
            MapWidth = mapWidth;
            MapHeight = mapHeight;

            for (int i = 0; i < MapHeight; i++)
                for (int j = 0; j < MapWidth; j++)
                    Layout[j, i] = ' ';
        }

        public int MapHeight { get; private set; }
        public int MapWidth { get; private set; }

        /// <summary>
        /// Sets a value for an index in the Layout array.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetIndexValue(int x, int y, char value) => Layout[x, y] = value;
        /// <summary>
        /// Changes the size of the map layout.
        /// </summary>
        /// <param name="newHeight"></param>
        /// <param name="newWidth"></param>
        public void ChangeSize(int newWidth, int newHeight)
        {
            char[,] previousLayout = Layout;
            Layout = new char[newHeight, newWidth];

            for(int i = 0; i < newHeight; i++)
            {
                for (int j = 0; j < newWidth; j++)
                {
                    if (j < MapWidth && i < MapHeight)
                        Layout[j, i] = previousLayout[j, i];
                    else
                        Layout[j, i] = ' ';
                }
            }
            MapHeight = newHeight;
            MapWidth = newWidth;
        }
        /// <summary>
        /// Clears all the fields inside the Layout property.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < MapHeight; i++)
                for (int j = 0; j < MapWidth; j++)
                    Layout[j, i] = ' ';
        }
    }
}
