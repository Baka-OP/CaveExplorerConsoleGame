using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using Cave_Explorer.Helpers;

namespace Cave_Explorer.Models
{
    class MapEditor
    {
        public int MapHeight { get; private set; }
        public int MapWidth { get; private set; }

        public string entitiesText;
        public List<MapTemplate> Templates { get; private set; }
        /// <summary>
        /// Instantiates a new editor.
        /// When loading a map, make sure it's parsable, or unpredictable things or crashes might happen.
        /// </summary>
        /// <param name="nameOrPath">If no \ is present, creates a new map, if \ is present, loads the map from the specified path.</param>
        public MapEditor(string nameOrPath)
        {
            Templates = new List<MapTemplate>();
            if (nameOrPath.Contains('\\'))
            {
                string[] files = Directory.GetFiles(nameOrPath);
                foreach(string s in files)
                {
                    if(s.ToLower().EndsWith("template.txt"))
                        Templates.Add(new MapTemplate(s, StringToColor.ConvertFromString(s.Split('\\')[^1].ToLower().Replace("template.txt", ""))));
                }
            }
            else
            {
                Templates.Add(new MapTemplate(60, 20));
            }

            //Get widest and tallest map length
            foreach(MapTemplate t in Templates)
            {
                if (t.MapHeight > MapHeight)
                    MapHeight = t.MapHeight;
                if (t.MapWidth > MapWidth)
                    MapWidth = t.MapWidth;
            }

            //Apply lengths to all templates
            foreach(MapTemplate t in Templates)
            {
                if (t.MapHeight != MapHeight || t.MapWidth != MapWidth)
                    t.ChangeSize(MapWidth, MapHeight);
            }
            CheckValues();
        }
        /// <summary>
        /// Checks the values inside all of the MapTemplates to see if there are any positions that are in occupied by multiple templates. If any are found, clears those positions.
        /// </summary>
        private void CheckValues()
        {
            for(int i = 0; i < MapHeight; i++)
            {
                for (int j = 0; j < MapWidth; j++)
                {
                    int occupiedCount = 0;
                    for(int templateNum = 0; templateNum < Templates.Count; templateNum++)
                    {
                        if (Templates[templateNum].Layout[j, i] != ' ')
                            occupiedCount++;
                    }
                    if (occupiedCount > 1)
                        ClearValueFromAll(j, i);
                }
            }
        }
        /// <summary>
        /// Clears the value of an index from all of the templates.
        /// </summary>
        public void ClearValueFromAll(int left, int top)
        {
            foreach(MapTemplate mt in Templates)
            {
                mt.Layout[left, top] = ' ';
            }
        }
        public void SetMapSize(int newWidth, int newHeight)
        {
            foreach(MapTemplate t in Templates)
            {
                t.ChangeSize(newWidth, newHeight);
            }
            MapHeight = newHeight;
            MapWidth = newWidth;
        }
        public void SetValue(int left, int top, char value, ConsoleColor color)
        {
            foreach(MapTemplate t in Templates)
            {
                if(t.Color == color)
                {
                    t.SetIndexValue(left, top, value);
                    return;
                }
            }
            MapTemplate template = new MapTemplate(MapWidth, MapHeight, color);
            template.SetIndexValue(left, top, value);
            Templates.Add(template);
        }
    }
}
