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
        public string MapDirectoryPath { get; private set; }
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
                MapDirectoryPath = nameOrPath;
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
                MapDirectoryPath = Environment.CurrentDirectory + "\\Map layouts\\Custom\\" + nameOrPath;
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
        /// Checks the values inside all of the MapTemplates to see if there are any positions that are occupied by multiple templates. If any are found, clears those positions.
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
        /// <summary>
        /// Sets an index of the map with a specific character with a specific color. If that space is occupied, it is overwritten.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="value"></param>
        /// <param name="color"></param>
        public void SetIndex(int left, int top, char value, ConsoleColor color)
        {
            bool templateExists = false;
            foreach(MapTemplate t in Templates)
            {
                t.SetIndexValue(left, top, ' ');
                if(t.Color == color)
                {
                    t.SetIndexValue(left, top, value);
                    templateExists = true;
                }
            }
            if (!templateExists)
            {
                MapTemplate template = new MapTemplate(MapWidth, MapHeight, color);
                template.SetIndexValue(left, top, value);
                Templates.Add(template);
            }
        }

        public bool SaveMap()
        {
            //First create a backup in case saving fails.
            if(Directory.Exists(MapDirectoryPath))
                Directory.Move(MapDirectoryPath, Environment.CurrentDirectory + "\\Map layouts\\Custom\\Backup");

            try
            {
                Directory.CreateDirectory(MapDirectoryPath);
                if (!File.Exists(MapDirectoryPath + "\\Entities.txt"))
                    File.Create(MapDirectoryPath + "\\Entities.txt").Close();

                foreach (MapTemplate t in Templates)
                {
                    StringBuilder line = new StringBuilder();
                    for (int i = 0; i < t.MapHeight; i++)
                    {
                        for (int j = 0; j < t.MapWidth; j++)
                        {
                            line.Append(t.Layout[j, i]);
                        }
                        line.Append('\n');
                    }
                    line.Remove(line.Length - 1, 1);
                    File.WriteAllText(MapDirectoryPath + "\\" + t.Color.ToString() + "Template.txt", line.ToString());
                }
                Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(Environment.CurrentDirectory + "\\Map layouts\\Custom\\Backup", Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents);
                return true;
            }
            catch
            {
                Directory.Move(Environment.CurrentDirectory + "\\Map layouts\\Custom\\Backup", MapDirectoryPath);
                Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(MapDirectoryPath, Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents);
                return false;
            }
        }
    }
}
